
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers
{
    public class HomeController : Controller
    {
         public async Task<ActionResult> Home()
         { 
             var rand = new Random(); 
             var topSalesResultJson = await ProductServices.GetProductsFromApi(rand.Next(1,1400)); 
             
             var newArrivedResultJson = await ProductServices.GetProductsFromApi(rand.Next(1,1400));
             
             if (!topSalesResultJson.HasValues || !newArrivedResultJson.HasValues)
                 return RedirectToAction("Index", "ErrorPage");
             
             ViewData["test"] = topSalesResultJson["data"].ToString(); 
             ViewData["test2"] = newArrivedResultJson["data"].ToString();
             ViewData["token"] = Request.Cookies["accessToken"]?.Value ?? "";
             return View("Booker");
        }

    }
}