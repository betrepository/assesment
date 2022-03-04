using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BET_ecommerce_website.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal? Price { get; set; }

        public int TotalCarts { get; set; }

        public int? Quantity { get; set; }
    }
}

