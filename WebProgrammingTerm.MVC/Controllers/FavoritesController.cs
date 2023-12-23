using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class FavoritesController : Controller
{
    [HttpPost]
    public async Task<ActionResult> Index(string productId)
    {
        var accessToken = Request.Cookies["accessToken"].Value;

        if (accessToken is null)
            return Json(new { success = true,redir=Url.Action("SignIn","Account")});

        var jsonObject =await FavoritesService.AddToFavorites(productId, accessToken);

        if (!jsonObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (jsonObject["errors"].HasValues)
            return Json(new { success = true,redir=Url.Action("Index","ErrorPage")});

        var data = jsonObject["data"].ToObject<UserFavorites>();
        return Json(new { success = true});
    }
    
    
    
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var accessToken = Request.Cookies["accessToken"].Value;

        if (accessToken is null)
            return RedirectToAction("SignIn", "Account");

        var userFavoritesObject = await FavoritesService.GetFavorites(accessToken);

        if (!userFavoritesObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (userFavoritesObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");


        var productIds = userFavoritesObject["data"].ToObject<UserFavoritesListDto>();

        var products = new List<Product>();


        foreach (var id in productIds.ProductIds)
        {
             var product = ((await ProductServices.GetProduct(id))["data"].ToObject<Product>());
             product.Id = id;
             products.Add(product);
        }
        return View("MyFavorites" ,model:products);
    }

  
    
}