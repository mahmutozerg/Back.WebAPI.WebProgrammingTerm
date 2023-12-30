using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.Mappers;
using SharedLibrary.Models;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

public class DepotController:CustomControllerBase
{
    private readonly IDepotService _depotService;

    public DepotController(IDepotService depotService)
    {
        _depotService = depotService;
    }
    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Add(DepotAddDto depotAddDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var depot = DepotMapper.ToDepot(depotAddDto);
        return CreateActionResult(await _depotService.AddAsync(depot,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
    
    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(DepotUpdateDto depotUpdateDto)
    {
         return CreateActionResult(await _depotService.UpdateAsync(depotUpdateDto,(ClaimsIdentity)User.Identity));
    }
    
    
    [HttpGet("[action]")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> GetDepots()
    {
        var depots = await _depotService.Where(d => d != null && !d.IsDeleted).ToListAsync();
        
        return CreateActionResult(CustomResponseListDataDto<Depot>.Success(depots,200));
    }
    
    
}