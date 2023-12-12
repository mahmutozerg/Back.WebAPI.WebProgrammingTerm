using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using SharedLibrary.Mappers;

using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

[Authorize(Roles = "Admin")]
public class DepotController:CustomControllerBase
{
    private readonly IDepotService _depotService;

    public DepotController(IDepotService depotService)
    {
        _depotService = depotService;
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Add(DepotAddDto depotAddDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var depot = DepotMapper.ToDepot(depotAddDto);
        return CreateActionResult(await _depotService.AddAsync(depot,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(DepotUpdateDto depotUpdateDto)
    {
         return CreateActionResult(await _depotService.UpdateAsync(depotUpdateDto,(ClaimsIdentity)User.Identity));
    }

}