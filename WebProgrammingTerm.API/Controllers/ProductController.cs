using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;
public class ProductController:CustomControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost]
    [Authorize(Roles = "CompanyUser,Admin,Company")]

    public async Task<IActionResult> Add( ProductAddDto productAddDto)
    {
        return CreateActionResult(
            await _productService.AddAsync(
                productAddDto,
                (ClaimsIdentity)User.Identity));
    }
    
    [HttpPut]
    [Authorize(Roles = "CompanyUser,Admin,Company")]

    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
 
         return CreateActionResult(await _productService.UpdateAsync(productUpdateDto,(ClaimsIdentity)User.Identity));
     }
    
    
    [HttpGet]
    public async Task<IActionResult> GetProductsByPage(int page)
    {
        var aA = await _productService.GetProductsByPage(page);
        return CreateActionResult(aA);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {

        var a = await _productService.GetProductWithComments(id);

        return CreateActionResult(a);
    }
    
    
    [HttpGet("{name}/{page:int}")]
    public async Task<IActionResult> GetProductByName(int page ,string name)
    {

        var a = await _productService.GetProductByName(page, name);

        return CreateActionResult(a);
    }
}