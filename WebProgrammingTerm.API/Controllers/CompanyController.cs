using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

[Authorize(Roles = "Company,Admin")]

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
    public async Task<IActionResult> Add(CompanyAddDto companyAddDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var company = CompanyMapper.ToCompany(companyAddDto);
        return CreateActionResult(await _companyService.AddAsync(company,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(CompanyUpdateDto companyUpdateDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _companyService.UpdateAsync(companyUpdateDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));

    }
    [HttpDelete("[action]")]
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
    public async Task<IActionResult> CreateCompanyUser(CompanyUserDto companyUserDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _companyUserService.AddAsync(companyUserDto,
            claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
    
}