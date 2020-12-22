using ShopEn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopEn.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        QLBHDTEntities db = new QLBHDTEntities();
        private int number = 12;
        [HttpGet]
        public ActionResult Index(int id)
        {
            int d =db.SANPHAMs.ToList().Count;
            List<SANPHAM> sanPhams = db.SANPHAMs.OrderBy(c=>c.MASP).Skip(id*number).Take(number).ToList();
            ViewBag.Page = Math.Ceiling(d / number*1.0);
            return View(sanPhams);
        }

    }
}