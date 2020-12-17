using ShopEn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopEn.Controllers
{
    public class SingleProductController : Controller
    {
        // GET: SingleProduct
        QLBHDTEntities db = new QLBHDTEntities();
        [HttpGet]
        public ActionResult Index(int id)
        {
            SANPHAM s = db.SANPHAMs.Find(id);
            return View(s);
        }
    }
}