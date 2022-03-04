using BET_ecommerce_website.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BET_ecommerce_website.Helpers
{
    public static class CartHelpers
    {
        public static async Task<string> getTotalsPeruserAsync(string user)
        {
            string _baseurl = ConfigurationManager.AppSettings["BetApi"];
            string total = "0";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Response = await client.GetAsync("Product/TotalPricePerUser?user=" + user);

                if (Response.IsSuccessStatusCode)
                {
                    var results = Response.Content.ReadAsStringAsync().Result;

                    total = results;
                }

                return formatTotal(total.ToString());
            }           
        }

        public static string formatTotal(String total)
        { 
            total = total.Trim(new Char[] { ' ', '"', ' ' });

            var convertedTotal = Convert.ToDecimal(total);

            convertedTotal = Math.Round(convertedTotal,2);                

            return convertedTotal.ToString();
        }
    }
}