using BET.eCommerce.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BET_ecommerce_web_api.Model;
using System.Linq;


namespace BET_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductController : ControllerBase
    {

        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        // GET: Product
        [HttpGet]
        [Route("Products")]
        public List<ProductModel> getProducts(string user)
        {
            List<ProductModel> results = (from product in _productRepository.GetAll()

                                           select new ProductModel()
                                        {
                                                ID = product.ID,
                                                Name = product.Name,
                                                Description = product.Description,
                                                Price = product.Price,
                                                Quantity = product.Quantity,
                                                TotalCarts = _productRepository.getTotalCarts(user, product.ID)

                                        }).ToList();
            return results;
        }

        [HttpPost]
        [Route("AddProduct")]
        public List<Product> AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
            return _productRepository.GetAll();
        }

        [HttpGet]
        [Route("ProductDetails")]
        public Product getProductDetails(int Id)
        {
            return _productRepository.ProductDetails(Id);
        }


        [HttpPut]
        [Route("updateProduct")]
        public Product updateProduct(Product product)
        {
            return _productRepository.updateProduct(product);
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public List<Product> deleteProduct(int id)
        {
            _productRepository.deleteProduct(id);
            return _productRepository.GetAll();
        }

        [HttpPost]
        [Route("AddToCart")]
        public int AddToCart(CartModel model)
        {  
           _productRepository.AddToCart(model.user,model.productId);

            return _productRepository.getTotalCarts(model.user, model.productId);
        }

        [HttpPost]
        [Route("RemoveFromCart")]
        public int RemoveFromCart(CartModel model)
        {
            _productRepository.RemoveFromCart(model.user, model.productId);

            return _productRepository.getTotalCarts(model.user, model.productId);
        }

        [HttpGet]
        [Route("CheckOut")]
        public void CheckOut(string user, string ordernumber)
        {
            _productRepository.CheckOut(user,ordernumber);            
        }

        [HttpGet]
        [Route("ProductsAvailable")]
        public void ProductsAvailable()
        {
            _productRepository.ProductsAvailable();
        }

        [HttpGet]
        [Route("TotalCarts")]
        public int  getTotalCarts(CartModel model)
        {
            return _productRepository.getTotalCarts(model.user,model.productId);
        }

        [HttpGet]
        [Route("TotalPricePerUser")]
        public string TotalPricePerUser(string user)
        {
            return _productRepository.TotalPricePerUser(user);
        }

        [HttpGet]
        [Route("ViewShoppingCart")]
        public List<ShoppingCartModel> ViewShoppingCart(string user)
        {
            return _productRepository.ViewShoppingCart(user);
        }

        [HttpGet]
        [Route("MyOrders")]
        public List<OrderModel> MyOrders(string user)
        {
            return _productRepository.MyOrders(user);
        }        
    }
}

    