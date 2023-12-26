using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class AdminController : Controller
{
    public async Task<ActionResult> Index()
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;

        if (accessToken is null)
            return RedirectToAction("Home", "Home");

        var roleObject = await AdminServices.GetRole(accessToken);

        if (roleObject["data"].ToObject<string>() != "Admin")
            return RedirectToAction("Home", "Home");
        return View();
    }
    
    public async Task<ActionResult> Products(string name="", int page=1)
    {
        if (page < 1)
        {
            page = 1;
        }
        var accessToken = Request.Cookies["accessToken"]?.Value;

        if (accessToken is null)
            return RedirectToAction("Home", "Home");

        var roleObject = await AdminServices.GetRole(accessToken);

        if (roleObject["data"].ToObject<string>() != "Admin")
            return RedirectToAction("Home", "Home");


        var productObject = await AdminServices.GetProducts(name, page, accessToken);
        var products = productObject["data"].ToObject<List<ProductGetDto>>();

        ViewData["AdminProductPage"] = page;
        ViewData["AdminProductName"] = name;
        return View(products);
    }
    
    public async Task<ActionResult> UpdateProduct(string id)
    {

        var accessToken = Request.Cookies["accessToken"]?.Value;

        if (accessToken is null)
            return RedirectToAction("Home", "Home");

        var roleObject = await AdminServices.GetRole(accessToken);

        if (roleObject["data"].ToObject<string>() != "Admin")
            return RedirectToAction("Home", "Home");

        var productObject = await ProductServices.GetProduct(id);

        if (!productObject.HasValues) 
            return RedirectToAction("Index", "ErrorPage");


        if (productObject["errors"].HasValues)
        {
            return RedirectToAction("Index", "ErrorPage");
        }

        var product = productObject["data"].ToObject<ProductUpdateDto>();
        product.TargetProductId = id;
        product.Size = productObject["data"]["productDetail"]["size"].ToString();
        
        return View(product);
    }
    
    [HttpPost]
    public async Task<ActionResult> UpdateProduct(ProductUpdateDto product)
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;

        if (accessToken is null)
            return RedirectToAction("Home", "Home");

        var roleObject = await AdminServices.GetRole(accessToken);

        if (roleObject["data"].ToObject<string>() != "Admin")
            return RedirectToAction("Home", "Home");


        var updateResultObject = await ProductServices.UpdateProduct(product, accessToken);

        if (!updateResultObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (updateResultObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");
            
        
        return RedirectToAction("Products");
    }

    [HttpPost]
    public async Task<ActionResult> DeleteProduct(string id)
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;

        if (accessToken is null)
            return RedirectToAction("Home", "Home");

        var roleObject = await AdminServices.GetRole(accessToken);

        if (roleObject["data"].ToObject<string>() != "Admin")
            return RedirectToAction("Home", "Home");

        var deleteResult = await ProductServices.DeleteProductById(id, accessToken);
        
        var responseData = new {success = true, redir = @Url.Action("Products" ) };
        return Json(responseData);

    }
}