using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopEn.Models;
using ShopEn.Code;

namespace ShopEn.Controllers
{

    public class CartController : Controller
    {
        // GET: Cart

        QLBHDTEntities db = new QLBHDTEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public string ChangeQuantity(int id, string type, int sl)
        {
            double amount;
            List<Item> cart = (List<Item>)Session["cart"];
            if (type == TypeItem.SanPham.ToString())
            {
                int index = isExistSanPham(id);
                if (index != -1)
                {
                    cart[index].quantity = sl;
                }
                else
                {
                    SANPHAM sp = db.SANPHAMs.Find(id);
                    cart.Add(new ItemSP { sanpham = sp, type = TypeItem.SanPham, quantity = sl, gia = Convert.ToDouble(sp.GIA) });
                }
                amount = Convert.ToDouble(db.SANPHAMs.Find(id).GIA * sl);
            }
            else
            {
                int index = isExistCombo(id);
                if (index != -1)
                {
                    cart[index].quantity = sl;
                }
                else
                {
                    COMBO c = db.COMBOes.Find(id);
                    cart.Add(new ItemCB { combo = db.COMBOes.Find(id), quantity = sl, type = TypeItem.Combo, gia = Convert.ToDouble(c.GIA) });
                }
                amount = Convert.ToDouble(db.COMBOes.Find(id).GIA * sl);
            }
            Session["cart"] = cart;
            double total = 0;
            foreach (var item in ((List<Item>)Session["cart"]))
            {
                //s += ((ItemSP)item).sanpham.TENSP + ":" + item.quantity + "\n";
                total += item.quantity * item.gia;
            }
            return total +" "+ amount;
        }
        [HttpGet]
        public string Buy(int id, string type, int sl)
        {
            if (Session["cart"] == null)
            {

                List<Item> cart = new List<Item>();
                Item i;
                if (type == TypeItem.SanPham.ToString())
                {
                    SANPHAM sp = db.SANPHAMs.Find(id);
                    i = new ItemSP { sanpham = sp, type = TypeItem.SanPham, quantity = sl, gia = Convert.ToDouble(sp.GIA) };
                }
                else
                {
                    COMBO cb = db.COMBOes.Find(id);
                    i = new ItemCB { combo = db.COMBOes.Find(id), type = TypeItem.Combo, quantity = sl, gia = Convert.ToDouble(cb.GIA) };

                }
                cart.Add(i);
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                if (type == TypeItem.SanPham.ToString())
                {
                    int index = isExistSanPham(id);
                    if (index != -1)
                    {
                        cart[index].quantity = sl;
                    }
                    else
                    {
                        SANPHAM sp = db.SANPHAMs.Find(id);
                        cart.Add(new ItemSP { sanpham = sp, type = TypeItem.SanPham, quantity = sl, gia = Convert.ToDouble(sp.GIA) });
                    }
                }
                else
                {
                    int index = isExistCombo(id);
                    if (index != -1)
                    {
                        cart[index].quantity = sl;
                    }
                    else
                    {
                        COMBO c = db.COMBOes.Find(id);
                        cart.Add(new ItemCB { combo = db.COMBOes.Find(id), quantity = sl, type = TypeItem.Combo, gia=Convert.ToDouble(c.GIA) });
                    }
                }
                Session["cart"] = cart;
            }
            //string s = "SL san pham " + ((List<Item>)Session["cart"]).Count + "\n";
            string s = "";
            double total = 0;
            foreach (var item in ((List<Item>)Session["cart"]))
            {
                //s += ((ItemSP)item).sanpham.TENSP + ":" + item.quantity + "\n";
                total += item.quantity * item.gia;
            }
            return total+"";
        }

        public ActionResult Remove(int index)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            //int index = -1;
            //if (type == TypeItem.SanPham.ToString())
            //    index= isExistSanPham(id);
            //else
            //    index = isExistCombo(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        private int isExistSanPham(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].type == TypeItem.SanPham)
                {
                    ItemSP s = (ItemSP)cart[i];
                    if (s.sanpham.MASP == id)
                        return i;
                }
            return -1;
        }
        private int isExistCombo(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].type == TypeItem.Combo)
                    if (((ItemCB)cart[i]).combo.MACB == id)
                        return i;
            return -1;
        }
    }
}