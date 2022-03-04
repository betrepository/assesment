using System;
using System.Web.Mvc;
using System.Net.Http;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using BET_ecommerce_website.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace BET_ecommerce_website.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        string _baseurl = ConfigurationManager.AppSettings["BetApi"];
  
            // GET: Cart
        public async  Task<JsonResult> AddToCartAsync(int productId)
        {

            var totalCarts = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);


                var model = new Models.CartModel()
                {
                    user = System.Web.HttpContext.Current.User.Identity.Name,
                    productId = productId
                };

                HttpResponseMessage Response = client.PostAsync("Product/AddToCart", new StringContent(new JavaScriptSerializer().Serialize(model), Encoding.UTF8, "application/json")).Result;

                if (Response.IsSuccessStatusCode)
                {
                    var results = Response.Content.ReadAsStringAsync().Result;
                    totalCarts = results;
                }
            }
            var totalPrice = await Helpers.CartHelpers.getTotalsPeruserAsync(System.Web.HttpContext.Current.User.Identity.Name);

            var res = new { carts = totalCarts, price = totalPrice };

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> RemoveFromCartAsync(int productId) 
        {
           
            var totalCarts = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);

                var model = new Models.CartModel()
                {
                    user = System.Web.HttpContext.Current.User.Identity.Name,
                    productId = productId
                };

                HttpResponseMessage Response = client.PostAsync("Product/RemoveFromCart", new StringContent(new JavaScriptSerializer().Serialize(model), Encoding.UTF8, "application/json")).Result;

                if (Response.IsSuccessStatusCode)
                {
                    var results = Response.Content.ReadAsStringAsync().Result;
                    totalCarts = results;
                }
            }

            var totalPrice = await Helpers.CartHelpers.getTotalsPeruserAsync(System.Web.HttpContext.Current.User.Identity.Name);

            var res = new { carts = totalCarts, price = totalPrice };

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewShoppingCart()
        {
            List<ShoppingCartModel> shoppingCart = new List<ShoppingCartModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.GetAsync("Product/ViewShoppingCart?user=" + System.Web.HttpContext.Current.User.Identity.Name);

                if (Response.IsSuccessStatusCode)
                {
                    var results = Response.Content.ReadAsStringAsync().Result;
                    shoppingCart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(results);

                }

                ViewBag.TotalPrice = await Helpers.CartHelpers.getTotalsPeruserAsync(System.Web.HttpContext.Current.User.Identity.Name);

                return View(shoppingCart);
            }
        }

        public JsonResult ChectOut()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);

                Random rnd = new Random();

                int ordernumber = rnd.Next();

                var user = System.Web.HttpContext.Current.User.Identity.Name;

                var checkOutTask = client.GetAsync("Product/CheckOut?user=" + user + "&ordernumber="+ordernumber.ToString());

                var result = checkOutTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Helpers.EmailHelper.sendEmail(ordernumber,user);
                    return Json("You have check out successfull, congratulations!!!!", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Your check out failed, sorry!!!!", JsonRequestBehavior.AllowGet);
                }
            }           
        }
    }
}