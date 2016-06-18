using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanSach_MyClass.Models;

namespace BanSach_MyClass.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        db_myclassEntities db = new db_myclassEntities();
        public ActionResult Index()
        {
            var sach = db.Sachs.Where(s => s.LoaiSach == 1);
            return View(sach);
        }
        public ActionResult ViewDetail(int masach=0)
        {
            
            Sach sach = db.Sachs.SingleOrDefault(s=>s.MaSach==masach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ChuDe cd = db.ChuDes.Single(s => s.MaCD == sach.MaCD);
            ViewBag.TenCD = cd.TenCD;

            ViewBag.TenNXB = db.NXBs.Single(n => n.MaNXB == sach.MaNXB).TenNXB;

            sach.RelevantbBooks = db.Sachs.Where(x => x.MaCD == sach.MaCD ).ToList();

            return View(sach);

        }

        public ActionResult GetRelevantBooks(int maSach, int maCD)
        {
           var  RelevantbBooks = db.Sachs.Where(x => x.MaCD == maCD).ToList();
           return PartialView("RelevantBooks", RelevantbBooks);
        }
    }
}
