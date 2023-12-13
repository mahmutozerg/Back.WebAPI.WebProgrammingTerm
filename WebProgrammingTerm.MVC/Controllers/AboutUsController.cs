using System.Web.Mvc;

namespace WebProgrammingTerm.MVC.Controllers;

public class AboutUsController : Controller
{
    // GET
    public ActionResult AboutUsIndex()
    {
        return View();
    }
}