using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

[Authorize]
public class CompanyController:CustomControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Add(CompanyDto companyDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var company = CompanyMapper.ToCompany(companyDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return CreateActionResult(await _companyService.AddAsync(company));
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
        return CreateActionResult(CustomResponseListDataDto<Company>.Success(entities,200));

    }
}