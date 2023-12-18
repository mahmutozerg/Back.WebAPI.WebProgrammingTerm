using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

[Authorize]
public class LocationController:CustomControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    [HttpPut]
    public async Task<IActionResult> Add( LocationDto locationDto)
    {
        return CreateActionResult(await _locationService.AddAsync(locationDto,(ClaimsIdentity)User.Identity));
    }
    [HttpPost]
    public async Task<IActionResult> Update(LocationUpdateDto locationUpdateDto)
    {
        return CreateActionResult(await _locationService.UpdateAsync(locationUpdateDto,(ClaimsIdentity)User.Identity));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        return CreateActionResult(await _locationService.GetLocationsAsync((ClaimsIdentity)User.Identity));
    }
}