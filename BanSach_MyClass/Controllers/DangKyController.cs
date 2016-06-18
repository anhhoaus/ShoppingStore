using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanSach_MyClass.Models;   

namespace BanSach_MyClass.Controllers
{
    public class DangKyController : Controller
    {
        //
        // GET: /DangKy/
        db_myclassEntities db = new db_myclassEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(KhachHang kh)
        {
            KhachHang custummer = db.KhachHangs.Add(kh);
            db.SaveChanges();
            return View("DangNhap");
        }
        public ActionResult DangNhap()
        {
            return View();
        }
          [HttpPost]
        public ActionResult DangNhap(FormCollection frm)
        {
            string sTaiKhoan = frm["TaiKhoan"].ToString();
            string sMatKhau = frm.Get("MatKhau").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(s => s.TaiKhoan == sTaiKhoan && s.MatKhau == sMatKhau);
            if (kh != null)
            {
                             //membership
                //if (System.Web.Security.Membership.ValidateUser(sTaiKhoan, sMatKhau))
                //{
                //    System.Web.Security.FormsAuthentication.SetAuthCookie(sTaiKhoan, true);
                //}


                ViewBag.Message = "Đăng Nhập thành công";
                Session["TaiKhoan"] = kh;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Tài Khoản hoặc  Mật khẩu không đúng";

            }
            return View();
        }
    }
}
