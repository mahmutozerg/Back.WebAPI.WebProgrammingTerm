using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebProgrammingTerm.MVC.Services;

public class TokenServices
{
    private string Url = "https://localhost:7049/api/Auth/CreateToken";

    public async Task<List<HttpCookie>> GetUserTokens(string mail,string passwd)
    {

        using (var client = new HttpClient())
        {
            var loginDto = new
            {
                email = mail,
                password = passwd

            };

            var createUserJsonData = JsonConvert.SerializeObject(loginDto);

            var content = new StringContent(createUserJsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(Url, content);
            if (response.IsSuccessStatusCode)
            {            
                var jsonResult = JObject.Parse(await response.Content.ReadAsStringAsync());

                if (jsonResult is not null)
                {
                    try
                    {
                        var _accessToken = jsonResult["data"]["accesssToken"].ToString();
                        var _refreshToken = jsonResult["data"]["refreshToken"].ToString();
                        var accessTokenCookie = new HttpCookie("AuthToken")
                        {
                            Value = _accessToken,
                            HttpOnly = true,
                            Secure = true, // Set to true if using HTTPS
                        };

                        // Handle the refresh token as needed
                        var refreshTokenCookie = new HttpCookie("RefreshToken")
                        {
                            Value = _refreshToken,
                            HttpOnly = true,
                            Secure = true, // Set to true if using HTTPS
                        };

                        var cookies = new List<HttpCookie>();
                        cookies.Add(accessTokenCookie);
                        cookies.Add(refreshTokenCookie);
                        
                        return cookies;
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine($"Error accessing accesssToken property: {ex.Message}");
                        // Handle the exception or log the error as needed
                    }

                }
            }

            return null;
        }
    }
}