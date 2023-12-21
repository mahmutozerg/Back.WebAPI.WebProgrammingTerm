using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class FavoritesController : Controller
{
    [HttpPost]
    public async Task<ActionResult> Index(string productId)
    {
        var accessToken = Request.Cookies["accessToken"].Value;
        var jsonObject =await FavoritesService.AddToFavorites(productId, accessToken);

        if (!jsonObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (!jsonObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");

        var data = jsonObject["data"].ToObject<UserFavorites>();
        return View();
    }
}