using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedLibrary.DTO;

namespace WebProgrammingTerm.MVC.Services;

public static class CompanyServices
{
    
    private const string AddCompanyUrl = "https://localhost:7082/api/Company/Add";

    public static async Task<JObject> AddCompany(CompanyAddDto companyAddDto,string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var jsonData = JsonConvert.SerializeObject(companyAddDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(AddCompanyUrl,content);
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        return new JObject();
    }
}