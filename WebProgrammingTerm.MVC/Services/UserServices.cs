using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedLibrary.DTO;
using WebProgrammingTerm.MVC.Models;

namespace WebProgrammingTerm.MVC.Services;

public static class UserServices
{
    private const string CreateTokenUrl = "https://localhost:7049/api/Auth/CreateToken";
    private const string CreateUserUrl = "https://localhost:7049/api/User/CreateUser";
    public static async Task<JObject> SignInUser(LoginDto loginDto)
    {
        if (!IsValidUser(loginDto.Email, loginDto.Password))
            return new JObject();

        using (var client = new HttpClient())
        {
 
            var createUserJsonData = JsonConvert.SerializeObject(loginDto);

            var content = new StringContent(createUserJsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(CreateTokenUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = JObject.Parse(await response.Content.ReadAsStringAsync());
                return jsonResult;
            }

            return new JObject();
        }



    }

    public static async Task<JObject> SignUpUser(SignUpDto signUpDto)
    {
        if (!IsValidUser(signUpDto.Email, signUpDto.Password))
            return new JObject();

        using var client = new HttpClient();
        var createUserDto = new CreateUserDto()
        {
            Email = signUpDto.Email,
            Password = signUpDto.Password,
            FirstName = signUpDto.FirstName,
            LastName = signUpDto.LastName

        };

        var createUserJsonData = JsonConvert.SerializeObject(createUserDto);

        var content = new StringContent(createUserJsonData, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(CreateUserUrl, content);

        var jsonResult = JObject.Parse(await response.Content.ReadAsStringAsync());

        return jsonResult;

    }

    public static TokenDto GeTokenInfo(JObject jsonResult)
    {
        return new TokenDto()
        {
            AccessToken = jsonResult["data"]["accessToken"].ToString(),
            AccessTokenExpiration = DateTime.TryParse(jsonResult["data"]["accessTokenExpiration"]!.ToString(), out var expirationDate) ? expirationDate : DateTime.MinValue,
            RefreshToken = jsonResult["data"]["refreshToken"].ToString(),
            RefreshTokenExpiration = DateTime.TryParse(jsonResult["data"]["refreshTokenExpiration"].ToString(), out var ex) ? ex : DateTime.MinValue,
            
            
        };
    }
    public static List<HttpCookie> GetCookies(JObject jsonResult)
    {
        try
        {
            var tokenDto = GeTokenInfo(jsonResult);
                        
            var accessTokenCookie = new HttpCookie("accessToken")
            {
                
                Expires = tokenDto.AccessTokenExpiration,
                Value = tokenDto.AccessToken,
                HttpOnly = true,
                Secure = true, // Set to true if using HTTPS
                
            };

            // Handle the refresh token as needed
            var refreshTokenCookie = new HttpCookie("refreshToken")
            {
                Expires = tokenDto.RefreshTokenExpiration,
                Value = tokenDto.RefreshToken,
                HttpOnly = true,
                Secure = true, // Set to true if using HTTPS
            };

            var cookies = new List<HttpCookie>
            {
                accessTokenCookie,
                refreshTokenCookie
            };

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