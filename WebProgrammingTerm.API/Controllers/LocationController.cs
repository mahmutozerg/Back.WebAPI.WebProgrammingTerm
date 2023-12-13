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
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Add( LocationDto locationDto)
    {
        return CreateActionResult(await _locationService.AddAsync(locationDto,(ClaimsIdentity)User.Identity));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(LocationUpdateDto locationUpdateDto)
    {
        return CreateActionResult(await _locationService.UpdateAsync(locationUpdateDto,(ClaimsIdentity)User.Identity));
    }
}