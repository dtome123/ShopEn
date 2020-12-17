using ShopEn.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopEn.Models
{
    public class Item
    {
        public int quantity { get; set; }
        public TypeItem type { get; set; }
    }
    public class ItemSP : Item
    {
        public SANPHAM sanpham { get; set; }
    }
    public class ItemCB : Item
    {
        public COMBO combo { get; set; }
    }
}