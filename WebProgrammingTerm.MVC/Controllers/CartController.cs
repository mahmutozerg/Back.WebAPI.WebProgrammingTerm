using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.MVC.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class CartController : Controller
{
    public CartModel CartModel = new CartModel();
    [HttpPost]
    public async Task<ActionResult> AddToCart(List<string> productIds)
    {
        
        var accessToken = Request.Cookies["accessToken"]?.Value;


        if (accessToken is null)
            return RedirectToAction("SignIn", "Account");

        foreach (var id in productIds)
        {
            var productJsonObject = await ProductServices.GetProduct(id);
            if (!productJsonObject.HasValues)
                return RedirectToAction("Index", "ErrorPage");

            if (productJsonObject["errors"].HasValues)
                return RedirectToAction("Index", "ErrorPage");
            
            CartModel.Products.Add(productJsonObject["data"].ToObject<ProductWCommentDto>());
        }

        var userLocationsObject = await AddressServices.GetUserLocation(accessToken);
        
        if (!userLocationsObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (userLocationsObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");

        var locations = userLocationsObject["data"].ToObject<List<Location>>();
        CartModel.Locations.AddRange(locations);

        TempData["CartContent"] = CartModel;
        return Json(new { success = true,redir=@Url.Action("Index")});
    }
    
    public async Task<ActionResult> Index()
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;
        
        if (accessToken is null)
            return RedirectToAction("SignIn", "Account");
        
        var model = TempData.Peek("CartContent") as CartModel;
        return View("Index", model ?? new CartModel());
    }
    
    [HttpPost]
    public async Task<ActionResult> Index( CartModel cartModel)
    {
        
        var accessToken = Request.Cookies["accessToken"]?.Value;
        
        if (accessToken is null)
            return RedirectToAction("SignIn", "Account");
        
        var model = TempData["CartContent"] as CartModel;

        if (model == null)
            return RedirectToAction("Index", "ErrorPage");
        
        var orderAddDto = new OrderAddDto()
        {
            LocationId = cartModel.ClickedLocation,
            PaymentCard = cartModel.CCNumber,
            ProductIdList = model.Products.Select(p => p.ProductId).ToList(),
            Shipment = "BookerrExpress"
        };

        var result = await CartServices.AddToFavorites(orderAddDto, accessToken);

        if (!result.HasValues)
            return RedirectToAction("Index", "ErrorPage");
        if (result["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");

        TempData.Clear();
        return RedirectToAction("Home","Home");

    }

    
    [HttpPost]
    public async Task<ActionResult> Remove(string id)
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;
        
        if (accessToken is null)
            return RedirectToAction("SignIn", "Account");
        
        var model = TempData.Peek("CartContent") as CartModel;
        model.Products.Remove(model.Products.FirstOrDefault(p => p.ProductId == id));

        TempData["CartContent"] = model;
        return Json(new { success = true,redir=@Url.Action("Index")});
    }
}