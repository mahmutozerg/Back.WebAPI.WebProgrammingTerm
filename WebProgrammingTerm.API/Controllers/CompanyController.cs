using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

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

       return CreateActionResult(await _companyService.AddAsync(companyDto));
    }
}