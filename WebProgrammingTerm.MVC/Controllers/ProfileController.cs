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
            return RedirectToAction("SignIn", "Account");
        var userData = userJson["data"];
        var user = userData.ToObject<User>();
        return View(user);
    }
    
    
    [HttpPost]
    public async Task<ActionResult> SaveProfile(AppUserUpdateDto appUserUpdateDto)
    {
        var token = Request.Cookies["accessToken"]?.Value;

        var userJson = await UserServices.UpdateUserProfile(appUserUpdateDto,token);

        if (!userJson.HasValues)
            return RedirectToAction("SignIn", "Account");
        
        
        var userData = userJson["data"];
        var user = userData.ToObject<User>();
        return View("ProfileIndex",user);

    }
}