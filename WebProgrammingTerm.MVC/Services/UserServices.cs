using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebProgrammingTerm.MVC.Services;

public static class UserServices
{
    private const string CreateTokenUrl = "https://localhost:7049/api/Auth/CreateToken";
    private const string CreateUserUrl = "https://localhost:7049/api/User/CreateUser";
    public static async Task<JObject> SignInUser(string mail,string passwd)
    {
        if (!IsValidUser(mail, passwd))
            return null;

        using var client = new HttpClient();
        var loginDto = new
        {
            email = mail,
            password = passwd

        };

        var createUserJsonData = JsonConvert.SerializeObject(loginDto);

        var content = new StringContent(createUserJsonData, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(CreateTokenUrl, content);

        var jsonResult = JObject.Parse(await response.Content.ReadAsStringAsync());

        return jsonResult;

    }

    public static async Task<JObject> SignUpUser(string mail,string passwd)
    {
        if (!IsValidUser(mail, passwd))
            return null;

        using var client = new HttpClient();
        var loginDto = new
        {
            email = mail,
            password = passwd

        };

        var createUserJsonData = JsonConvert.SerializeObject(loginDto);

        var content = new StringContent(createUserJsonData, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(CreateUserUrl, content);

        var jsonResult = JObject.Parse(await response.Content.ReadAsStringAsync());

        return jsonResult;

    }
    public static List<HttpCookie> GetCookies(JObject jsonResult)
    {
        try
        {
            var _accessToken = jsonResult["data"]["accessToken"].ToString();
            var _accessTokenExp = jsonResult["data"]["accessTokenExpiration"]!.ToString();
            var _refreshToken = jsonResult["data"]["refreshToken"].ToString();
            var _refreshTokenxp = jsonResult["data"]["refreshTokenExpiration"].ToString();
                        
            var accessTokenCookie = new HttpCookie("accessToken")
            {
                Expires = DateTime.TryParse(_accessTokenExp, out var expirationDate) ? expirationDate : DateTime.MinValue,
                Value = _accessToken,
                HttpOnly = true,
                Secure = true, // Set to true if using HTTPS
            };

            // Handle the refresh token as needed
            var refreshTokenCookie = new HttpCookie("refreshToken")
            {
                Expires = DateTime.TryParse(_refreshTokenxp, out var ex) ? ex : DateTime.MinValue,
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

        return null;
    }
    
    private static bool IsValidUser(string email, string password)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}