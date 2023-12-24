using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebProgrammingTerm.MVC.Services
{
    public static class ProductServices
    {
        private static string Url = "https://localhost:7082/api/Product";
        
        public static async Task<JObject> GetProductsFromApi(int page =1)
        {
            using (var client = new HttpClient())
            {
                var pi = page.ToString();
                var response = await client.GetAsync(Url+$"?page={pi}");

                if (response.IsSuccessStatusCode)
                   return JObject.Parse(await response.Content.ReadAsStringAsync());
            }

            return new JObject();

        }
        
        
        public static async Task<JObject> GetProduct(string id)
        {
            using (var client = new HttpClient())
            {
                var a= Url + $"/{Uri.EscapeDataString(id)}";
                var response = await client.GetAsync(Url+$"/{Uri.EscapeDataString(id)}");

                if (response.IsSuccessStatusCode)
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
            }

            return new JObject();

        }

        public static async Task<JObject> GetProductFromName(string name,int page)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(Url + $"/{Uri.EscapeDataString(name)}/{page}");

                if (response.IsSuccessStatusCode)
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
            }

            return new JObject();
        }
    }
    
}