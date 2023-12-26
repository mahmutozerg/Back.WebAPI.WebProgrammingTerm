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
    private const string GetProductsByNameUrl = "https://localhost:7082/api/Admin/GetProductsByName";
    private const string GetUsersByEmailUrl = "https://localhost:7082/api/Admin/GetUsersByEmail";
    private const string GetUserByIdUrl = "https://localhost:7082/api/Admin/GetUserById";
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
                 url = GetProductsByNameUrl + $"?page={page}";
            }
            else
            {
                url = GetProductsByNameUrl + $"?page={page}&name={Uri.EscapeDataString(name)}";
            }
            
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        return new JObject();

    }
    
    
    public static async Task<JObject> GetUsersByEmail(string email,int page,string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var url = "";
            if (string.IsNullOrEmpty(email))
            {               
                url = GetUsersByEmailUrl + $"?page={page}";
            }
            else
            {
                url = GetUsersByEmailUrl + $"?page={page}&email={Uri.EscapeDataString(email)}";
            }
            
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        return new JObject();
    }
    
    public static async Task<JObject> GetUserById(string id,string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync(GetUserByIdUrl+"/"+Uri.EscapeDataString(id));
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        return new JObject();
    }
}