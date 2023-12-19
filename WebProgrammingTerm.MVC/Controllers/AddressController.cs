using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharedLibrary.Mappers;
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
            return RedirectToAction("Index", "ErrorPage");
        
        
        var locations = locationJson["data"].ToObject<List<Location>>();
        
        if (TempData.ContainsKey("show") && TempData.TryGetValue("PopUpMessage", out var popMes)  )
        {
            ViewData["show"] = true;
            ViewData["PopUpMessage"] = popMes.ToString();

            TempData.TryGetValue("index", out var index);
            ViewData["index"] = index;
        }

        return View(new AddressModel()
        {
            locations = locations
        });
    }

    [HttpPost]
    public async Task<ActionResult> AddAddress(AddressModel addressModel)
    {

        var token = Request.Cookies["accessToken"]?.Value;
        if (token is  null)
            return RedirectToAction("SignIn", "Account");

        if (addressModel.clickedIndex >= 0)
        {
            int.TryParse(addressModel.locationUpdateDto.City,out var cityId);
            addressModel.locationUpdateDto.City = addressModel.CityList[cityId-1].Text;
            
            var result = await AddressServices.UpdateUserLocation(addressModel.locationUpdateDto, token);

            if (!result.HasValues)
                return RedirectToAction("Index", "ErrorPage");
            
            if(result["errors"].HasValues)
                return RedirectToAction("Index", "ErrorPage");

            var model = result["data"].ToObject<Location>();
            TempData["PopUpMessage"] = "Address Updated";
            TempData["index"] = addressModel.clickedIndex;

        }
        else
        {
            int.TryParse(addressModel.locationUpdateDto.City,out var cityId);
            addressModel.locationUpdateDto.City = addressModel.CityList[cityId-1].Text;
           
            var result = await AddressServices.AddUserLocation(LocationMapper.ToLocationDto(addressModel.locationUpdateDto),token);
           
           if (!result.HasValues)
               return RedirectToAction("Index", "ErrorPage");
            
           if(result["errors"].HasValues)
               return RedirectToAction("Index", "ErrorPage");
           
           TempData["PopUpMessage"] = "Address created";
        }

        TempData["show"] = true;

        return RedirectToAction("Addresses");
    }


    [HttpPost]
    public async Task<ActionResult> DeleteAddress(string locationIdToDelete)
    {
        var token = Request.Cookies["accessToken"]?.Value;
        if (token is  null)
            return RedirectToAction("SignIn", "Account");
        

        var jObject = await AddressServices.DeleteUserLocation(locationIdToDelete, token);
        if (!jObject.HasValues  )
            return RedirectToAction("Index", "ErrorPage");

        if (jObject["errors"]!.HasValues) 
            return RedirectToAction("Addresses");
        
        TempData["show"] = true;
        TempData["PopUpMessage"] = "Your address successfully deleted";
        

        return RedirectToAction("Addresses");
    }
}