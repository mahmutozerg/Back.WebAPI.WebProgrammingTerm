using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;
[Authorize]
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
        var claimsIdentity = (ClaimsIdentity)User.Identity;
         return CreateActionResult(await _productService.AddAsync(productAddDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _productService.UpdateAsync(productUpdateDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
     }
}