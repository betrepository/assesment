using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BET_ecommerce_website.Models
{
    public class OrderModel
    {
        public string OrderNumber { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }        
        public string Status { get; set; }
    }
}