using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.MVC.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class ProfileController : Controller
{
    [HttpGet]
    public async Task<ActionResult> ProfileIndex()
    {
        if (TempData["ModelState"] is ModelStateDictionary modelState)
            ModelState.Merge(modelState);
        

        var token = Request.Cookies["accessToken"]?.Value;
        
        if (token is null) 
            return RedirectToAction("SignIn", "Account");
        
        var userJson = await UserServices.GetUserProfileInfo(token);
        
        if (!userJson.HasValues)
        {
            Response.Cookies["accessToken"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["refreshToken"].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("Index", "ErrorPage");

        }
        
        var userData = userJson["data"];
        var user = userData.ToObject<User>();
        
        
        return View(new UserModels()
        {
            User = user
        });
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
            return RedirectToAction("Index", "ErrorPage");
        
        var userRefreshTokenJson =await UserServices.CreateTokenByRefreshToken(refreshToken);
        
        Response.Cookies["accessToken"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["refreshToken"].Expires = DateTime.Now.AddDays(-1);
        
        if (!userRefreshTokenJson.HasValues)
            return RedirectToAction("Index", "ErrorPage");
        

        var cookies = UserServices.AddCookies(userRefreshTokenJson);
        foreach (var cookie in cookies)
        {
            Response.Cookies.Add(cookie);
        }
        appUserUpdateDto.BirthDate = birthdate;
        
        return RedirectToAction("ProfileIndex");

    }

    [HttpPost]
    public async Task<ActionResult> UpdatePassword(UserUpdatePasswordDto userUpdatePasswordDto)
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;
        var refreshToken = Request.Cookies["refreshToken"]?.Value;
        
        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            return RedirectToAction("SignIn", "Account");

        if (string.CompareOrdinal(userUpdatePasswordDto.NewPassword, userUpdatePasswordDto.NewPasswordConfirm) != 0)
        {
            ModelState.AddModelError("Password","Password and Confirm password does not match");
            return RedirectToAction("ProfileIndex");
        }

        var jsonObject = await UserServices.UpdateUserPassword(userUpdatePasswordDto, accessToken);
        if (!jsonObject.HasValues)
        {
            Response.Cookies["accessToken"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["refreshToken"].Expires = DateTime.Now.AddDays(-1);
            RedirectToAction("Index", "ErrorPage");
        }

        if (!jsonObject["errors"]!.HasValues)
        {
            Response.Cookies["accessToken"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["refreshToken"].Expires = DateTime.Now.AddDays(-1); 
            
            /*
                    !!! Uncomment this if you don't want user to login after the password change
                    if you uncomment it user will continue to its session with new tokens 
            
            var cookies =UserServices.AddCookies(jsonObject);
            
            foreach (var cookie in cookies)
            {
                Response.Cookies.Add(cookie);
            }
            */
        }
        else
        {
            foreach (var error in jsonObject["errors"])
            {
                if (error.ToString().Contains("ssword")  )
                    ModelState.AddModelError("Password",$"{error}");
            }
        }
        TempData["ModelState"] = ModelState;

        return RedirectToAction("ProfileIndex");


    }
}