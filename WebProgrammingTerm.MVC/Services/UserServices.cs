﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
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
    private const string GetUserBasicInfo = "https://localhost:7082/api/User/GetById";
    private const string UpdateUserInfo = "https://localhost:7082/api/User/Update";
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
    public static List<HttpCookie> AddCookies(JObject jsonResult)
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


    public static async Task<JObject> GetUserProfileInfo(string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync(GetUserBasicInfo);

            if (response.IsSuccessStatusCode)
                return JObject.Parse(await response.Content.ReadAsStringAsync());
            
        }

        return new JObject();
    }
public static async Task<JObject> UpdateUserProfile(AppUserUpdateDto appUserUpdateDto, string token)
{
    using (var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Create a JObject to store the properties
        JObject jsonObject = new JObject();

        // Get the type of the AppUserUpdateDto
        Type type = typeof(AppUserUpdateDto);

        // Get all public properties of the type
        PropertyInfo[] properties = type.GetProperties();

        // Iterate over each property
        foreach (PropertyInfo property in properties)
        {
            // Get the value of the property
            object value = property.GetValue(appUserUpdateDto);

            // Add property to the JObject only if it has a non-null value
            if (value != null)
            {
                JToken jToken = JToken.FromObject(value);
                jsonObject[property.Name] = jToken;
            }
        }

        // Convert the JObject to JSON string
        var jsonData = jsonObject.ToString();

        // Create StringContent with JSON data
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        // Send the request and handle the response
        var response = await client.PostAsync(UpdateUserInfo, content);

        if (response.IsSuccessStatusCode)
        {
            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
    }

    return new JObject();
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