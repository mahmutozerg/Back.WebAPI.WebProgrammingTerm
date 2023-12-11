using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

[Authorize(Roles = "Admin,CompanyUser,Company")]

public class CompanyController:CustomControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly ICompanyUserService _companyUserService;
    
    public CompanyController(ICompanyService companyService, ICompanyUserService companyUserService)
    {
        _companyService = companyService;
        _companyUserService = companyUserService;
    }
    
    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add(CompanyAddDto companyAddDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        
        return CreateActionResult(await _companyService.AddAsync(companyAddDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value,accessToken));
    }
    
    [HttpPost("[action]")]
    [Authorize(Roles = "Company,Admin")]
    public async Task<IActionResult> Update(CompanyUpdateDto companyUpdateDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _companyService.UpdateAsync(companyUpdateDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));

    }
    [HttpDelete("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;

        return CreateActionResult( await _companyService.Remove(id,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCompanyInfo(string id)
    {
        var entities =  _companyService.Where(c => c.Id == id && !c.IsDeleted).ToList();
        return CreateActionResult(CustomResponseListDataDto<Company>.Success(entities,ResponseCodes.Ok));

    }
    [HttpPost("[action]")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> CreateCompanyUser(CompanyUserDto companyUserDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        return CreateActionResult(await _companyUserService.AddAsync(companyUserDto,(ClaimsIdentity)User.Identity,accessToken));
    }
    
}