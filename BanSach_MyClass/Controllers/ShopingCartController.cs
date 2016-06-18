using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BanSach_MyClass.Models;

namespace BanSach_MyClass.Controllers
{
    public class ShopingCartController : Controller
    {
        db_myclassEntities db = new db_myclassEntities();
       
        // kiểm tra xem có session gio hang
        //sau đó lấy giỏ hàng dạng danh sách<gioHang>
        public List<ShopingCart> LayGioHang()
        {
            List<ShopingCart> litCart = Session["ShopingCart"] as List<ShopingCart>;
            if (litCart == null)
            {
                //nếu giỏ hàng chưa tồn tại thì mình tiến hành tạo giỏ hàng
                litCart = new List<ShopingCart>();
                // session để lưu toàn bộ trên trang 
                Session["ShopingCart"] = litCart;
            }
            return litCart; // ngc lại nếu giỏ hàng đẫ tồn tại 
        }

        //[Authorize]
        public ActionResult AddShopingCart(int iMaSach, string url)
        {
            //kt xem khách hang có truyền mã sp vào ko
            Sach sach = db.Sachs.SingleOrDefault(s => s.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy sesion của gio hang == hàm LayGioHang
            List<ShopingCart> litCart = LayGioHang();
            //kt xem mã sách đc mua chưa nếu chưa mua thì thêm vào, nếu có mã hang rồi thì công thêm vào số lượng
            //tạo ra list chưa dc lưu vào csdl mà luu ở list

            ShopingCart sp = litCart.Find(c => c.iMaSach == iMaSach);
            if (sp == null)
            {
                sp = new ShopingCart(iMaSach);
                litCart.Add(sp);
                return Redirect(url);
            }
            else
            {
                sp.iMaSach ++;
                return Redirect(url);
            }
        }
        public ActionResult EditCart(int iMa, FormCollection f)
        {
            //kt xem có mấ sách hợp lệ ko nếu ko có trả về lỗi đg dẫn
            Sach sach = db.Sachs.SingleOrDefault(s => s.MaSach == iMa);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng và session
            List<ShopingCart> litCart = LayGioHang();
            //tìm sp có trong giỏ hàng ko phải trong db bây gio vẫn lưu hết trên classs
            //nếu có thì cho sửa số lượng xong trả về trang chi tiết giỏ hàng 
            ShopingCart sp = litCart.SingleOrDefault(u => u.iMaSach == iMa);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f.Get("txtSoLuong").ToString());
            }
            return RedirectToAction("ShopingCart");
        }

        public ActionResult DeleteShopingCart(int iMaSach)
        {
            Sach sach = db.Sachs.SingleOrDefault(s => s.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ShopingCart> litCart = LayGioHang();

            ShopingCart sp = litCart.SingleOrDefault(u => u.iMaSach == iMaSach);
            if (sp != null)
            {
                litCart.RemoveAll(n => n.iMaSach == iMaSach);
            }
            if (litCart.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            //sau đó return về action gio hang
            return RedirectToAction("ShopingCart");
        }

        //xay dung trang gio hang
        public ActionResult ShopingCart()
        {
            if (Session["ShopingCart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<ShopingCart> litCart = LayGioHang();
            return View(litCart);
        }
        // tinh tổng số lươngj
        public double TongTien()
        {
            double dTongTien = 0;
            List<ShopingCart> litCart = Session["ShopingCart"] as List<ShopingCart>;
            if (litCart != null)
            {
                dTongTien = litCart.Sum(s => s.dThanhTien);
            }
            return dTongTien;
        }
        public ActionResult suaGioHang()
        {
            if (Session["ShopingCart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<ShopingCart> litCart = LayGioHang();
            return View(litCart);
        }
    }
}
