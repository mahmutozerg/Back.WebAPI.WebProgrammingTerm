using System.Web.Mvc;

namespace WebProgrammingTerm.MVC.Controllers;

public class ProfileController : Controller
{
    // GET
    public ActionResult ProfileIndex()
    {
        return View();
    }
}