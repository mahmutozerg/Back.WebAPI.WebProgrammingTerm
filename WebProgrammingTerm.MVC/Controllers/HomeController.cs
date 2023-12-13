﻿
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

    }
}