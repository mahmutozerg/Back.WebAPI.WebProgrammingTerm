using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class AccountController : Controller
{
    // GET
    public ActionResult SignIn()
    {
        var token = Request.Cookies["accessToken"]?.Value;
 
        if (token is not null)
            return RedirectToAction("ProfileIndex", "Profile");

        return View();

    }
    
    [HttpPost]
    public async Task<ActionResult> SignIn(string email, string password)
    {

        var token = Request.Cookies["accessToken"]?.Value;
 
        if (token is not null)
            return RedirectToAction("Home", "Home");
        
        var result = await UserServices.SignInUser(email, password);

        if (result is null)
            return View("SignUp");

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
    public async Task<ActionResult> SignUp(string firstName, string lastName, string email, string password, string confirmPassword)
    {
        var result = await UserServices.SignUpUser(email, password);
        /*
         *todo şifreleri kıyasla
         *todo giris okeyse tokenleri setle
         *todo uyarı yazılarını duzenle
         */
        if (!result["errors"].HasValues)
        {

            return View();

        }

        foreach (var error in result["errors"])
        {
            if (error.ToString().Contains("Password"))
                ModelState.AddModelError("Password",$"{error}");

            if (error.ToString().Contains("Username") || error.ToString().Contains("ail"))
            {
                ModelState.AddModelError("mail",$"{"Email already exist"}");

            }

            if (!ViewData.ModelState.ContainsKey("mail"))
                ViewData["email"] = email;
            
            ViewData["FirstName"] = firstName;
            ViewData["LastName"] = lastName;
        }

        return View();
    }
}