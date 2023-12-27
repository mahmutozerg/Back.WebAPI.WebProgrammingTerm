using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;
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
    [Authorize(Roles = "Admin,Company")]

    public async Task<IActionResult> Add( ProductAddDto productAddDto)
    {
        return CreateActionResult(
            await _productService.AddAsync(
                productAddDto,
                (ClaimsIdentity)User.Identity));
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin,Company")]

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
    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetPureProduct(string id)
    {

        var product = await _productService.GetProductWithCompany(id);
        if (product is null)
        {
            return CreateActionResult(CustomResponseDto<Product>.Fail(ResponseMessages.ProductNotFound,
                ResponseCodes.NotFound));
        }
        return CreateActionResult(CustomResponseDto<Product>.Success(product,ResponseCodes.Ok));
    }
    
    [HttpGet("{name}/{page:int}")]
    public async Task<IActionResult> GetProductByName(int page ,string name)
    {

        var a = await _productService.GetProductByName(page, name);
        return CreateActionResult(a);
    }
    [HttpGet("[action]/{category}/{page:int}")]
    public async Task<IActionResult> GetProductByCategory(int page ,string category)
    {

        var a = await _productService.GetProductByCategory(page, category);
        return CreateActionResult(a);
    }
    [HttpDelete]
    [Authorize(Roles = "Admin,Company")]
    public async Task<IActionResult> DeleteProductById([FromQuery]string id)
    {

        var a = await _productService.DeleteProductById(id);
        return CreateActionResult(a);
    }
}