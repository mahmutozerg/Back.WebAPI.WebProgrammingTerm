﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

[Authorize(Roles = "Company,Admin")]
public class ProductDetailController:CustomControllerBase
{
    private readonly IProductDetailService _productDetailService;

    public ProductDetailController(IProductDetailService productDetailService)
    {
        _productDetailService = productDetailService;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Add( ProductDetailAddDto productAddDto)
    {
        return CreateActionResult(await _productDetailService.AddAsync(productAddDto,(ClaimsIdentity)User.Identity));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(ProductDetailUpdateDto productDetailUpdateDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _productDetailService.UpdateAsync(productDetailUpdateDto,(ClaimsIdentity)User.Identity));
    }
}