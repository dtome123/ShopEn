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
        public ActionResult Buy(int id, string type)
        {
            
            if (Session["cart"] == null)
            {
                
                List<Item> cart = new List<Item>();
                Item i;
                if (type==TypeItem.SanPham.ToString())
                {
                     i= new ItemSP { sanpham = db.SANPHAMs.Find(id), type = TypeItem.Combo, quantity = 1 };
                }
                else
                {
                    i = new ItemCB { combo = db.COMBOes.Find(id), type = TypeItem.Combo, quantity = 1 };
                    
                }
                cart.Add(i);
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                if (type==TypeItem.SanPham.ToString())
                {
                    int index = isExistSanPham(id);
                    if (index != -1)
                    {
                        cart[index].quantity++;
                    }
                    else
                    {
                        cart.Add(new ItemSP { sanpham = db.SANPHAMs.Find(id), quantity = 1, type = TypeItem.SanPham });
                    }
                }
                else
                {
                    int index = isExistCombo(id);
                    if (index != -1)
                    {
                        cart[index].quantity++;
                    }
                    else
                    {
                        cart.Add(new ItemCB { combo = db.COMBOes.Find(id), quantity = 1, type = TypeItem.Combo });
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
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
                if(cart[i].type==TypeItem.SanPham)
                {
                    ItemSP s = (ItemSP)cart[i];
                    if (s.sanpham.MASP.Equals(id))
                        return i;
                }
            return -1;
        }
        private int isExistCombo(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if(cart[i].type ==TypeItem.Combo)
                    if (((ItemCB)cart[i]).combo.MACB.Equals(id))
                        return i;
            return -1;
        }
    }
}