using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BET_ecommerce_web_api.Model
{
    public class ProductModel
    {       
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal? Price { get; set; }

        public int TotalCarts { get; set; }



        public int? Quantity { get; set; }        
    }
}
