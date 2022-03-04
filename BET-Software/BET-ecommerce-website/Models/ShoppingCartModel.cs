using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BET_ecommerce_website.Models
{
    public class ShoppingCartModel
    {
        public string productName { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Total { get; set; }
    }
}