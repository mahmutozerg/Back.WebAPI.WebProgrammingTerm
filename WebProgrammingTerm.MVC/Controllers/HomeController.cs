
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
            var jsonResult = await _productServices.GetProductsFromApi(1);
            var jsonResult2 = await _productServices.GetProductsFromApi(3); 

            if (jsonResult is not null)
            {
                ViewData["test"] = jsonResult["data"].ToString();
                ViewData["test2"] = jsonResult2["data"].ToString();
            }
            return View("Booker");
        }
        public async Task<ActionResult> Test()
        {
            var cookies = await UserServices.SetUserTokens("user@example.com", "!Mahmut3590");

            if (cookies is not null)
            {
                Response.Cookies.Add(cookies[0]);
                Response.Cookies.Add(cookies[1]);
            }


            return View();
        }

    }
}