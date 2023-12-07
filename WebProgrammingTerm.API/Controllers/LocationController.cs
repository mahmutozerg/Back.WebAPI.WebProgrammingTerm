using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Core.DTO;
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
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _locationService.AddAsync(locationDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(LocationUpdateDto locationUpdateDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _locationService.UpdateAsync(locationUpdateDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
}