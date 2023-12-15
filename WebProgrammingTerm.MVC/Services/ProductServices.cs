using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebProgrammingTerm.MVC.Services
{
    public class ProductServices
    {
        private string Url = "https://localhost:7082/api/Product";
        
        public async Task<JObject> GetProductsFromApi(int page =1)
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
    }
}