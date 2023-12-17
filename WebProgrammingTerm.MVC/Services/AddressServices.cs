using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebProgrammingTerm.MVC.Services;

public static class AddressServices
{
    private const string CreateLocationUrl = "https://localhost:7082/api/Location/Add";
    private const string UpdateLocationUrl = "https://localhost:7082/api/Location/Update";
    private const string GetLocationUrl = "https://localhost:7082/api/Location";


    public static async Task<JObject> GetUserLocation(string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync(GetLocationUrl);
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        return new JObject();

    }
}