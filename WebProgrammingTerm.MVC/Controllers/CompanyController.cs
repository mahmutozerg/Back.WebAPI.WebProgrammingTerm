using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.MVC.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class CompanyController : Controller
{
    public async Task<ActionResult> Index()
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;

        if (accessToken is null)
            return RedirectToAction("Home", "Home");

        var roleObject = await AdminServices.GetRole(accessToken);

        return roleObject["data"].ToObject<string>() != "Company" ? RedirectToAction("Home", "Home") : RedirectToAction("Products");
    }
    
    public async Task<ActionResult> Products()
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;

        if (accessToken is null)
            return RedirectToAction("Home", "Home");

        var roleObject = await AdminServices.GetRole(accessToken);

        if (roleObject["data"].ToObject<string>() != "Company")
            return RedirectToAction("Home", "Home");

        var productObject = await CompanyServices.GetCompanyProducts(accessToken);

        if (!productObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (productObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");


        var products = productObject["data"].ToObject<List<Product>>();
        return View(products);
    }
    
    public async Task<ActionResult> AddProducts()
    {
        var accessToken = Request.Cookies["accessToken"]?.Value;

        if (accessToken is null)
            return RedirectToAction("Home", "Home");

        var roleObject = await AdminServices.GetRole(accessToken);

        if (roleObject["data"].ToObject<string>() != "Company")
            return RedirectToAction("Home", "Home");

        var depotObject = await DepotServices.GetFavorites(accessToken);

        if (!depotObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (depotObject["errors"].HasValues)
            return RedirectToAction("Index", "ErrorPage");


        var depotList = depotObject["data"].ToObject<List<Depot>>();
        
        
        
        return View(new CompanyProductAdd()
        {
            Depots = depotList,
            ProductAddDto = new ProductAddDto()
        });
    }
    
    [HttpPost]
    public async Task<ActionResult> AddProducts(CompanyProductAdd model)
    {

        var accessToken = Request.Cookies["accessToken"]?.Value;

        if (accessToken is null)
            return RedirectToAction("Home", "Home");

        var roleObject = await AdminServices.GetRole(accessToken);

        if (roleObject["data"].ToObject<string>() != "Company")
            return RedirectToAction("Home", "Home");



        if (model is null)
            return RedirectToAction("Index", "ErrorPage");


        var productResultObject = await ProductServices.AddProduct(model.ProductAddDto, accessToken);

        if (!productResultObject.HasValues)
            return RedirectToAction("Index", "ErrorPage");
        
        return productResultObject["errors"].HasValues ? RedirectToAction("Index", "ErrorPage") : RedirectToAction("Products");
    }

}