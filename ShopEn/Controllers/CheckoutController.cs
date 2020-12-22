using ShopEn.Code;
using ShopEn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopEn.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        QLBHDTEntities db = new QLBHDTEntities();
        public ActionResult Index()
        {
            ViewBag.Total = ((List<Item>)Session["cart"]).Sum(c=>c.gia*c.quantity);
            return View();
        }
        [HttpPost]
        public ActionResult Add()
        {
            if (Session["cart"] != null)
            {
                HOADON h = new HOADON();
                h.MATT = 1;
                h.MAKH = 1;
                h.NGAYLAPHD = DateTime.Now;
                db.HOADONs.Add(h);
                db.SaveChanges();

                List<CTHD> chitiets = new List<CTHD>();
                
                foreach(var item in ((List<Item>)Session["cart"]))
                {
                    CTHD t = new CTHD();
                    t.MAHD = h.MAHD;
                    t.GIA = Convert.ToDecimal(item.gia);
                    t.SOLUONG = item.quantity;
                    if (item.type == TypeItem.SanPham)
                    {
                        t.MASP = ((ItemSP)item).sanpham.MASP;
                    }
                    else
                    {
                        t.MACB = ((ItemCB)item).combo.MACB;
                    }
                    db.CTHDs.Add(t);
                }
                db.SaveChanges();
                Session["cart"] = null;
                return RedirectToAction("Info", "Checkout", new { hd = h.MAHD});
            }
            else
                return RedirectToAction("Index","Home",null);
        }
        public ActionResult Info(int hd)
        {
            ViewBag.Id = hd;
            return View();
        }
    }
}