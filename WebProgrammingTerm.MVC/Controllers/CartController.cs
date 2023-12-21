using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class CartController : Controller
{
    [HttpPost]
    public async Task<ActionResult> AddToCart(List<string> productIds)
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;
        if (accessToken is null)
            return RedirectToAction("SignIn", "Account");

        var productlist = new List<ProductWCommentDto>();
        foreach (var id in productIds)
        {
            var productJsonObject = await ProductServices.GetProduct(id);
            if (!productJsonObject.HasValues)
                return RedirectToAction("Index", "ErrorPage");

            if (productJsonObject["errors"].HasValues)
                return RedirectToAction("Index", "ErrorPage");
            
            productlist.Add(productJsonObject["data"].ToObject<ProductWCommentDto>());
        }

        var userLocationsObject = await AddressServices.GetUserLocation(accessToken);
        
        if (!userLocationsObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (userLocationsObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");

        var locations = userLocationsObject["data"].ToObject<List<Location>>();
        return Json(new { success = true });
    }
}