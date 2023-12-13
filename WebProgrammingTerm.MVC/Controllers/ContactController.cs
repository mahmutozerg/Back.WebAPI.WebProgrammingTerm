using System.Web.Mvc;

namespace WebProgrammingTerm.MVC.Controllers;

public class ContactController : Controller
{
    // GET
    public ActionResult ContactIndex()
    {
        return View();
    }
}