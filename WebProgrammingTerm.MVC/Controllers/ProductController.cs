using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.MVC.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class ProductController : Controller
{
    public async Task<ActionResult> Index(string id)
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;
        var productJsonObject = await ProductServices.GetProduct(id);

        if (!productJsonObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (productJsonObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");


        if (accessToken is not null)
        {
            var userJsonObject = await FavoritesService.GetFavorites(accessToken);
        
            if (!userJsonObject.HasValues)
                return RedirectToAction("Index", "ErrorPage");

            if (userJsonObject["errors"].HasValues)
                return RedirectToAction("Index", "ErrorPage");
            var model = new ProductModel()
            {
                ProductWGetDto = productJsonObject["data"].ToObject<ProductWCommentDto>(),
                UserFavoritesDto = userJsonObject["data"].ToObject<UserFavoritesListDto>()
            };
            model.ProductWGetDto.Size = productJsonObject["data"]["productDetail"]["size"].ToString();

            return View(model);

        }
        else
        {
            var model = new ProductModel()
            {
                ProductWGetDto = productJsonObject["data"].ToObject<ProductWCommentDto>(),
                UserFavoritesDto = new UserFavoritesListDto()
            };
            return View(model);
        }
        
    }
    public async Task<ActionResult> Search(string searchTerm = "",int page = 1)
    {

        if (page < 1)
        {
            page = 1;
        }
        var productObject = await ProductServices.GetProductFromName(searchTerm, page);

        if (!productObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (productObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");


        ViewData["searchterm"] = searchTerm;
        ViewData["page"] = page;
        var products = productObject["data"].ToObject<List<ProductGetDto>>() ?? new List<ProductGetDto>();



        return View(products);
    }

    public async Task<ActionResult> SearchByCategory(string searchTerm = "",int page = 1)
    {

        if (page < 1)
        {
            page = 1;
        }
        var productObject = await ProductServices.GetProductFromCategory(searchTerm, page);

        if (!productObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (productObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");


        ViewData["searchterm"] = searchTerm;
        ViewData["page"] = page;

        var products = productObject["data"].ToObject<List<ProductGetDto>>();

        if (products is null)
        {
            products = new List<ProductGetDto>();
        }
        
        if (products.Count ==0 && page >2)
        {
            ViewData["NXT"] = false;

            return RedirectToAction("SearchByCategory", new { searchTerm = searchTerm, page = page-1 });
            
        }
        ViewData["NXT"] = true;

        return View("Search",products);
    }

 
}