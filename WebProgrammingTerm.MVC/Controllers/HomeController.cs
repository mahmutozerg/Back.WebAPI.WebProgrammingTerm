
using System.Web.Mvc;

namespace WebProgrammingTerm.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Booker");
        }


    }
}