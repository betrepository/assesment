using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BET.eCommerce.Data.Repositories
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
