using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanSach_MyClass.Models;

namespace BanSach_MyClass.Controllers
{
    public class MyPartialController : Controller
    {
        //
        // GET: /MyPartial/
        db_myclassEntities db = new db_myclassEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTopPartial()
        {
            return View();
        }
        public ActionResult MenuMain()
        {
            return View();
        }

        public ActionResult Slide()
        {
            return View();
        }

        public ActionResult Topic()
        {
            var topic = db.ChuDes.OrderBy(t=>t.TenCD).ToList();
            return View(topic);
        }

        public ActionResult Publishing()
        {
            return View();
        }
    }
}
