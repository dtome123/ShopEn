using ShopEn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopEn.Code;

namespace ShopEn.Controllers
{
    public class SingleProductController : Controller
    {
        // GET: SingleProduct
        QLBHDTEntities db = new QLBHDTEntities();
        [HttpGet]
        public ActionResult Index(int id,string type)
        {
            if(type==TypeItem.SanPham.ToString())
            {
                SANPHAM s = db.SANPHAMs.Find(id);
                ViewBag.Id = s.MASP;
                ViewBag.Type = TypeItem.SanPham;
                ViewBag.MaxQuan = s.SOLUONG;
                return View(s);
            }
            else
            {
                COMBO c = db.COMBOes.Find(id);
                ViewBag.Id = c.MACB;
                ViewBag.Type = TypeItem.Combo;
                return View(c);
            }
        }

    }
}