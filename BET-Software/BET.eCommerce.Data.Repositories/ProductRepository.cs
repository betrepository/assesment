using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BET.eCommerce.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {        
        private BET _db = new BET();

        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public Product ProductDetails(int id)
        {
            return _db.Products.Find(id);
        }

        public List<Product> AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return _db.Products.ToList();
        }

        public Product updateProduct(Product product)
        {
            var existingProduct = _db.Products.Find(product.ID);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Quantity = product.Quantity;              
                existingProduct.Price = product.Price;
                _db.SaveChanges();
            }

            return existingProduct;
        }   

        public void deleteProduct(int id)
        {
            var product = _db.Products.Where(s => s.ID == id).FirstOrDefault();
            _db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
            _db.SaveChanges();
        }

        public void AddToCart(string user, int productId)
        {
            var uc = new User_Carts();
            uc.ProductID = productId;
            uc.UserID = user;
            uc.CheckedOut = false;
            uc.DateCreated = DateTime.Now;
            uc.DateUpdated = DateTime.Now;
            _db.User_Carts.Add(uc);
            _db.SaveChanges();         
        }

        public void CheckOut(string user, string ordernumber)
        {
            var userCarts = _db.User_Carts.Where(uc => uc.UserID == user && uc.CheckedOut == false);

            foreach (var item in userCarts)
            {
                item.CheckedOut = true;
                item.DateUpdated = DateTime.Now;

                _db.Orders.Add(new Order() { 
                       OrderNumber = ordernumber,
                       User_CartID = item.ID
                });
            }

            _db.SaveChanges();
        }   

        public void RemoveFromCart(string user, int productId)
        {
            var userCart = _db.User_Carts.Where(s => s.UserID == user && s.ProductID == productId).FirstOrDefault();
            _db.Entry(userCart).State = System.Data.Entity.EntityState.Deleted;
            _db.SaveChanges();
        }

        public int getTotalCarts(string user, int productId)
        {
            return _db.User_Carts.Where(uc => uc.UserID == user && uc.ProductID == productId && uc.CheckedOut == false).Count();
        }

        public string TotalPricePerUser(string user)
        {
           var totals =  (from p in _db.Products
                         join uc in _db.User_Carts on p.ID equals uc.ProductID
                         where uc.UserID == user && uc.CheckedOut == false 
                         group p by p.ID into g
                         
                         select new
                         {
                             Item = g.Key,
                             total = g.Sum(price => price.Price)
                         }).ToList().Sum(t => t.total);

            return totals.ToString();
        }

        public List<ShoppingCartModel> ViewShoppingCart(string user)
        {
            var shoppingCartTotals = (from p in _db.Products
                          join uc in _db.User_Carts on p.ID equals uc.ProductID
                          where uc.UserID == user && uc.CheckedOut == false
                          group p by p.ID into g

                          select new
                          {                            
                              productName = g.Where(n => n.ID == g.Key).FirstOrDefault().Name,  
                              Price = g.Where(n => n.ID == g.Key).FirstOrDefault().Price,
                              Quantity = g.Where(n => n.ID == g.Key).Count(),
                              Total = g.Sum(price => price.Price)
                          }).ToList();

            List<ShoppingCartModel> model = new List<ShoppingCartModel>();
            
            foreach (var shoppingCartTotal in shoppingCartTotals)
            {
                model.Add(
                    new ShoppingCartModel()
                    {
                        productName = shoppingCartTotal.productName,
                        Price = shoppingCartTotal.Price,
                        Quantity = shoppingCartTotal.Quantity,
                        Total = shoppingCartTotal.Total
                    }
                );
            }

            return model;
        }

        public List<OrderModel> MyOrders(string user)
        {  
            var orders = (from o in _db.Orders
                          join uc in _db.User_Carts on o.User_CartID equals uc.ID
                          where uc.UserID == user && uc.CheckedOut == true
                          group o by o.OrderNumber into g

                          select new
                          {
                              OrderNumber = g.Where(n => n.OrderNumber == g.Key).FirstOrDefault().OrderNumber,               
                              Status = "In Progress"
                          }).ToList();

            List<OrderModel> model = new List<OrderModel>();

            foreach (var order in orders)
            {
                model.Add(
                    new OrderModel()
                    {
                        OrderNumber = order.OrderNumber,
                        Status = order.Status
                    }
                );
            }

            return model;
        }

        public bool ProductsAvailable()
        {
            throw new NotImplementedException();
        }
    }
}
