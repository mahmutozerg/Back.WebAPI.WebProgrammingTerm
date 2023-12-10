using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;
[Authorize(Roles = "CompanyUser,Admin,Company")]
public class ProductController:CustomControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Add( ProductAddDto productAddDto)
    {
        return CreateActionResult(
            await _productService.AddAsync(
                productAddDto,
                (ClaimsIdentity)User.Identity));
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
 
         return CreateActionResult(await _productService.UpdateAsync(productUpdateDto,(ClaimsIdentity)User.Identity));
     }
}