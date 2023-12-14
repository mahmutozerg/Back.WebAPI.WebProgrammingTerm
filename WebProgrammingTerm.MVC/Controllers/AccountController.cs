using System.Linq;
using System.Threading.Tasks;
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
            
            ModelState.AddModelError("LoginError","PleaseCheck your credentials");
            return View("SignIn",loginDto);

        }
        var cookies = UserServices.GetCookies(result);
        foreach (var cookie in cookies)
        {
            Response.Cookies.Add(cookie);
        }

        return RedirectToAction("Home", "Home");
    }
    
    public  ActionResult SignUp()
    {

        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> SignUp(SignUpDto signUpDto)
    {
        if (string.CompareOrdinal(signUpDto.Password, signUpDto.ConfirmPassword) != 0)
        {
            ViewData["email"] = signUpDto.Email;
            ViewData["FirstName"] = signUpDto.FirstName;
            ViewData["LastName"] = signUpDto.LastName;
            ModelState.AddModelError("Password","Password and Confirm Password must match");
            return View(signUpDto);
        }
        
        var result = await UserServices.SignUpUser(signUpDto);
        /*
         *todo giris okeyse tokenleri setle
         *todo uyarı yazılarını duzenle
         */
        if (!result["errors"].HasValues)
        {
            var cookies = UserServices.GetCookies(result);
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
                ModelState.AddModelError("mail",$"{"Email already exist"}");

            }
            
        }
        if (!ViewData.ModelState.ContainsKey("mail"))
                ViewData["email"] = signUpDto.Email;
            
        ViewData["FirstName"] = signUpDto.FirstName;
        ViewData["LastName"] = signUpDto.LastName;
        return View(signUpDto);
    }
}