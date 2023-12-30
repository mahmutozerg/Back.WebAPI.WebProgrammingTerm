using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedLibrary.DTO;

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
        public static async Task<JObject> GetProductFromCategory(string category,int page)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(Url + $"/GetProductByCategory/{Uri.EscapeDataString(category)}/{page}");

                if (response.IsSuccessStatusCode)
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
            }

            return new JObject();
        }
        public static async Task<JObject> UpdateProduct(ProductUpdateDto productUpdateDto,string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var jsonData = JsonConvert.SerializeObject(productUpdateDto);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(Url,content);

                if (response.IsSuccessStatusCode)
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
            }

            return new JObject();

        }
        
        
        public static async Task<JObject> DeleteProductById(string id,string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.DeleteAsync(Url + $"?id={Uri.EscapeDataString(id)}");

                if (response.IsSuccessStatusCode)
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
            }

            return new JObject();

        }
        
        
        public static async Task<JObject> AddProduct(ProductAddDto productAddDto,string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var jsonData = JsonConvert.SerializeObject(productAddDto);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");               
                var response = await client.PostAsync(Url,content);

                if (response.IsSuccessStatusCode)
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
            }

            return new JObject();

        }
    }
    

    
}