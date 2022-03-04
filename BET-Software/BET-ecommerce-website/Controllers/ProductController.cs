using BET_ecommerce_website.Models;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Mvc;
using System.Net.Http.Headers;
using BET.eCommerce.Data.Repositories;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Text;
using System.Web;

namespace BET_ecommerce_website.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        string _baseurl = ConfigurationManager.AppSettings["BetApi"];
        // GET: Product
        public async Task<ActionResult> Index()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();              
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage Response = await client.GetAsync("Product/products?user=" + System.Web.HttpContext.Current.User.Identity.Name);
               
                if (Response.IsSuccessStatusCode)
                {                   
                    var results = Response.Content.ReadAsStringAsync().Result;
                    products = JsonConvert.DeserializeObject<List<ProductViewModel>>(results);

                }
            
                ViewBag.TotalPrice = await Helpers.CartHelpers.getTotalsPeruserAsync(System.Web.HttpContext.Current.User.Identity.Name);

                return View(products);
            }            
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ProductViewModel product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);                

                var response = client.PostAsync("Product/AddProduct", new StringContent(new JavaScriptSerializer().Serialize(product), Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }          
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Details(int Id)
        {
            ProductViewModel product = new ProductViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.GetAsync("Product/ProductDetails?Id=" + Id);

                if (Response.IsSuccessStatusCode)
                {
                    var results = Response.Content.ReadAsStringAsync().Result;                  
                    product = JsonConvert.DeserializeObject<ProductViewModel>(results);
                }

                return View(product);
            }
        }   

        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            ProductViewModel product = new ProductViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.GetAsync("Product/ProductDetails?Id=" + Id);

                if (Response.IsSuccessStatusCode)
                {
                    var results = Response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<ProductViewModel>(results);
                }
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);

                var response = client.PutAsync("Product/updateProduct", new StringContent(new JavaScriptSerializer().Serialize(product), Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
   
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
              
                var deleteTask = client.DeleteAsync("Product/deleteProduct?id=" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }      
    }
}