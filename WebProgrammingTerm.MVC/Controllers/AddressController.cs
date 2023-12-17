using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebProgrammingTerm.MVC.Models;
using WebProgrammingTerm.MVC.Services;

namespace WebProgrammingTerm.MVC.Controllers;

public class AddressController : Controller
{
    // GET
    public async Task<ActionResult> Addresses()
    {
        var token = Request.Cookies["accessToken"]?.Value;
        if (token is  null)
            return RedirectToAction("SignIn", "Account");       
        
        var locationJson = await AddressServices.GetUserLocation(token);
        if (!locationJson.HasValues)
            return RedirectToAction("Index", "ErrorPage");

        if (locationJson["errors"].HasValues)
        {
            return RedirectToAction("Index", "ErrorPage");
        }
        
        var locations = locationJson["data"].ToObject<List<Location>>();
        return View(new AddressModel()
        {
            locations = locations
        });
    }
}