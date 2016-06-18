using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanSach_MyClass.Models;

namespace BanSach_MyClass.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        public List<GioHang> LayGioHang()
        {
            List<GioHang> litCart = Session["GioHang"] as List<GioHang>;
            if (litCart == null)
            {
                //nếu giỏ hàng chưa tồn tại thì mình tiến hành tạo giỏ hàng
                litCart = new List<GioHang>();
                // session để lưu toàn bộ trên trang 
                Session["GioHang"] = litCart;
            }
            return litCart; // ngc lại nếu giỏ hàng đẫ tồn tại 
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lst = LayGioHang();
            return View(lst);
        }

    }
}
