using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.MVC.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class AccountController : Controller
{
    public ActionResult SignIn()
    {
        var token = Request.Cookies["accessToken"]?.Value;
 
        if (token is not null)
            return RedirectToAction("ProfileIndex", "Profile");

        return View();

    }
    
    [HttpPost]
    public async Task<ActionResult> SignIn(LoginDto loginDto)
    {

        var token = Request.Cookies["accessToken"]?.Value;
 
        if (token is not null)
            return RedirectToAction("Home", "Home");
        
        var result = await UserServices.SignInUser(loginDto);

        if (!result.HasValues )
        {
            
            ModelState.AddModelError("LoginError","Please Check your credentials");
            return View("SignIn",loginDto);

        }
        var cookies = UserServices.AddCookies(result);
        foreach (var cookie in cookies)
        {
            Response.Cookies.Add(cookie);
        }

        return RedirectToAction("Home", "Home");
    }
    
    public  ActionResult SignUp()
    {
        var token = Request.Cookies["accessToken"]?.Value;
 
        if (token is not null)
            return RedirectToAction("Home", "Home");
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> SignUp(SignUpDto signUpDto)
    {
        if (string.CompareOrdinal(signUpDto.Password, signUpDto.ConfirmPassword) != 0)
        {
            ModelState.AddModelError("Password","Password and Confirm Password must match");
            return View(signUpDto);
        }
        
        var result = await UserServices.SignUpUser(signUpDto);
        /*
         *todo giris okeyse tokenleri setle
         *todo uyarı yazılarını duzenle
         */
        if (!result.HasValues)
            return RedirectToAction("Index", "ErrorPage");
        
        if (!result["errors"].HasValues)
        {
            var cookies = UserServices.AddCookies(result);
            foreach (var cookie in cookies)
            {
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Home", "Home");
        }

        foreach (var error in result["errors"])
        {
            if (error.ToString().Contains("Password"))
                ModelState.AddModelError("Password",$"{error}");

            if (error.ToString().Contains("Username") || error.ToString().Contains("ail"))
            {
                ModelState.AddModelError("mail","Email already exist");

            }
            
        }

            

        return View(signUpDto);
    }


    [HttpGet]
    public ActionResult SignOut()
    {
        var token = Request.Cookies["accessToken"]?.Value;
        var rtoken = Request.Cookies["refreshToken"]?.Value;

        if (token is not null)
        {
            var accessTokenCookie = new HttpCookie("accessToken")
            {
                Expires = DateTime.Now.AddDays(-1),
                Path = "/",
                HttpOnly = true,
                Secure = true,
            };
            Response.Cookies.Add(accessTokenCookie);
        }

        if (rtoken is not null)
        {
            var refreshTokenCookie = new HttpCookie("refreshToken")
            {
                Expires = DateTime.Now.AddDays(-1),
                Path = "/",
                HttpOnly = true,
                Secure = true,
            };
            Response.Cookies.Add(refreshTokenCookie);
        }

        return RedirectToAction("Home", "Home");
    }
}