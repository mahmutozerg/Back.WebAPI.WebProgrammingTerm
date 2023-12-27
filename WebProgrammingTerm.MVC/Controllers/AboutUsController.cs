using System.Web.Mvc;

namespace WebProgrammingTerm.MVC.Controllers;

public class AboutUsController : Controller
{
    public ActionResult AboutUsIndex()
    {
        return View();
    }
}