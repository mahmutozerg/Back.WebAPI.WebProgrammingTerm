﻿using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedLibrary.DTO;

namespace WebProgrammingTerm.MVC.Services;

public static class AddressServices
{
    private const string CreateLocationUrl = "https://localhost:7082/api/Location";
    private const string UpdateLocationUrl = "https://localhost:7082/api/Location";
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
    
    public static async Task<JObject>UpdateUserLocation(LocationUpdateDto locationUpdateDto,string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var jsonData = JsonConvert.SerializeObject(locationUpdateDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(UpdateLocationUrl,content);
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        return new JObject();

    }
    public static async Task<JObject>AddUserLocation(LocationDto locationDto,string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var jsonData = JsonConvert.SerializeObject(locationDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(GetLocationUrl,content);
            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        return new JObject();

    }

    
}