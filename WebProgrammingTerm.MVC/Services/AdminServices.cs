using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedLibrary.DTO;

namespace WebProgrammingTerm.MVC.Services;

public static class AdminServices
{
    private const string GetRoleUrl = "https://localhost:7049/api/Auth/GetRole";
    private const string GetProductsByName = "https://localhost:7082/api/Admin/GetProductsByName";
    public static async Task<JObject> GetRole(string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync(GetRoleUrl);
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        return new JObject();

    }
    public static async Task<JObject> GetProducts(string name,int page,string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var url = "";
            if (string.IsNullOrEmpty(name))
            {               
                 url = GetProductsByName + $"?page={page}";
            }
            else
            {
                url = GetProductsByName + $"?page={page}&name={Uri.EscapeDataString(name)}";
            }
            
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        return new JObject();

    }
}