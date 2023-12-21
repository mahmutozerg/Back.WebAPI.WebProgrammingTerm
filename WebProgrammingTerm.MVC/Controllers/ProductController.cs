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



 
}