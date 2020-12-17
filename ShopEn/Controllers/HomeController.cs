using ShopEn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopEn.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QLBHDTEntities db = new QLBHDTEntities();
        public ActionResult Index()
        {
            List<SANPHAM> sanPhams = db.SANPHAMs.Take(6).OrderByDescending(c=>c.MASP).ToList();
            return View(sanPhams);
        }
        
    }
}