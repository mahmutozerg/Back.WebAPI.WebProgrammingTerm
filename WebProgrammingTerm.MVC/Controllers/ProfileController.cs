using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class ProfileController : Controller
{
    [HttpGet]
    public async Task<ActionResult> ProfileIndex()
    {
        var token = Request.Cookies["accessToken"]?.Value;
        if (token is null) 
            return RedirectToAction("SignIn", "Account");
        
        var userJson = await UserServices.GetUserProfileInfo(token);
        if (!userJson.HasValues)
        {
            Response.Cookies["accessToken"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["refreshToken"].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("SignIn", "Account");

        }
        var userData = userJson["data"];
        var user = userData.ToObject<User>();
        return View(user);
    }
    
    
    [HttpPost]
    public async Task<ActionResult> SaveProfile(AppUserUpdateDto appUserUpdateDto,string birthdate)
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;
        var refreshToken = Request.Cookies["refreshToken"]?.Value;
        
        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            return RedirectToAction("SignIn", "Account");

        
        var userJson = await UserServices.UpdateUserProfile(appUserUpdateDto,accessToken);

        if (!userJson.HasValues)
            return RedirectToAction("SignIn", "Account");
        
        var userRefreshTokenJson =await UserServices.CreateTokenByRefreshToken(refreshToken);
        Response.Cookies["accessToken"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["refreshToken"].Expires = DateTime.Now.AddDays(-1);
        if (!userRefreshTokenJson.HasValues)
        {
            return RedirectToAction("SignIn", "Account");
        }

        var cookies = UserServices.AddCookies(userRefreshTokenJson);
        foreach (var cookie in cookies)
        {
            Response.Cookies.Add(cookie);
        }
        appUserUpdateDto.BirthDate = birthdate;


        
        

        return RedirectToAction("ProfileIndex");

    }
}