using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebProgrammingTerm.MVC.Services;

public static class DepotServices
{
    private const string GetUrl = "https://localhost:7082/api/Depot/GetDepots";
    
    
    
    public static async Task<JObject> GetFavorites(string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync(GetUrl);
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        return new JObject();

    }
}