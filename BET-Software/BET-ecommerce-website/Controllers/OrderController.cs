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
    public class OrderController : Controller
    {
        string _baseurl = ConfigurationManager.AppSettings["BetApi"];
        // GET: Order
        public async Task<ActionResult> IndexAsync()
        {
            List<OrderModel> order = new List<OrderModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.GetAsync("Product/MyOrders?user=" + System.Web.HttpContext.Current.User.Identity.Name);

                if (Response.IsSuccessStatusCode)
                {
                    var results = Response.Content.ReadAsStringAsync().Result;
                    order = JsonConvert.DeserializeObject<List<OrderModel>>(results);
                }

                ViewBag.TotalPrice = await Helpers.CartHelpers.getTotalsPeruserAsync(System.Web.HttpContext.Current.User.Identity.Name);

                return View(order);
            }
        }
    }
}