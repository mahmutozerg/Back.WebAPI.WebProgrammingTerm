
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductServices _productServices = new ProductServices();
         public async Task<ActionResult> Home()
         { 
             var rand = new Random(); 
             // we have around 30k products each carousel contans 20 product so range is 1-1500
             var topSalesResultJson = await _productServices.GetProductsFromApi(rand.Next(1,1500)); 
             
             var newArrivedResultJson = await _productServices.GetProductsFromApi(rand.Next(1,1500));
             
             if (!topSalesResultJson.HasValues || !newArrivedResultJson.HasValues)
                return View("Index","Error");
             
             ViewData["test"] = topSalesResultJson["data"].ToString(); 
             ViewData["test2"] = newArrivedResultJson["data"].ToString(); 
             return View("Booker");
        }

    }
}