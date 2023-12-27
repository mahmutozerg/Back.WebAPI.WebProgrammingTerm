using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;

using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

[Authorize(Roles = "Admin,Company")]

public class CompanyController:CustomControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly IProductService _productService;
    
    public CompanyController(ICompanyService companyService, IProductService productService)
    {
        _companyService = companyService;
        _productService = productService;
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
        var entities =await  _companyService.Where(c => c.Id == id && !c.IsDeleted).ToListAsync();
        return CreateActionResult(CustomResponseListDataDto<Company>.Success(entities,ResponseCodes.Ok));

    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetCompanyProducts()
    {
        var name = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name)?.Value;

        if (name == null)
            return CreateActionResult(CustomResponseNoDataDto.Fail(ResponseCodes.NotFound,
                ResponseMessages.CompanyNotFound));
        
        var companyEntity = await _companyService.GetCompanyByName(name);
        
        if (companyEntity is null)
            return CreateActionResult(CustomResponseNoDataDto.Fail(ResponseCodes.NotFound,
                ResponseMessages.CompanyNotFound));
        
        var entities = await _productService.Where(p => p != null && p.CompanyId == companyEntity.Id)
            .Include(p=>p.ProductDetail)
            .ToListAsync();
        return CreateActionResult(CustomResponseListDataDto<Product>.Success(entities,ResponseCodes.Ok));



    }
    
}