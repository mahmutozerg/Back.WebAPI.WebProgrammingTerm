using System.Web.Mvc;

namespace WebProgrammingTerm.MVC.Controllers;

public class ErrorPageController : Controller
{
    // GET
    public ActionResult Index()
    {
        return View();
    }
}