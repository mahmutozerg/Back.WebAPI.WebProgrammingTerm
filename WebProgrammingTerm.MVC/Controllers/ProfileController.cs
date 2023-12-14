using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class ProfileController : Controller
{
    // GET
    public async Task<ActionResult> ProfileIndex()
    {
        var token = Request.Cookies["accessToken"]?.Value;
        if (token is null) 
            return RedirectToAction("SignIn", "Account");
        
        var userJson = await UserServices.GetUserProfileInfo(token);
        var userData = userJson["data"];
        if (userJson.HasValues)
        {
            var user = userData.ToObject<User>();
            return View(user);

        }

        return RedirectToAction("SignIn", "Account");
    }
}