using System.Web.Mvc;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class AccountController : Controller
{
    // GET
    public ActionResult SignInIndex()
    {
        return View();
    }
    
    [HttpPost]
    public ActionResult SignIn(string email, string password)
    {

        // Authentication failed
        // Add a model error or other logic to indicate the failure
        ModelState.AddModelError("", "Invalid email or password");

        // Return to the sign-in page
        return View("SignInIndex");
    }
}