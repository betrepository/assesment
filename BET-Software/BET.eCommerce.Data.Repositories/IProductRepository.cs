using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace BET.eCommerce.Data.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();        
        List<Product> AddProduct(Product product);
        Product ProductDetails(int id);
        Product updateProduct(Product product);
        void deleteProduct(int id);
        void AddToCart(string user, int productId);
        void RemoveFromCart(string user, int productId);
        int getTotalCarts(string user, int productId); 
        string TotalPricePerUser(string user);
        void CheckOut(string user, string ordernumber);
        bool ProductsAvailable();
        List<ShoppingCartModel> ViewShoppingCart(string user);
        List<OrderModel> MyOrders(string user);
    }
}
