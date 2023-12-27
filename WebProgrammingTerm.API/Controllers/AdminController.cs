using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.Mappers;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;



[Authorize(Roles = "Admin")]
public class AdminController:CustomControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDetailRepository _productDetailRepository;
    private readonly ICompanyService _companyService;
    private readonly IUserRepository _userRepository;

    public AdminController(IProductRepository productRepository, IProductDetailRepository productDetailRepository, IUserRepository userRepository, ICompanyService companyService)
    {
        _productRepository = productRepository;
        _productDetailRepository = productDetailRepository;
        _userRepository = userRepository;
        _companyService = companyService;
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductsByName([FromQuery]int page=1 ,[FromQuery]string? name ="")
    {
        if (string.IsNullOrEmpty(name))
        {
            var productsPage = await _productRepository.GetProducstByPageAdmin(page);
            var  dtosPage = productsPage.Select(product => ProductMapper.ToAddDto(product)).ToList();
        
            return CreateActionResult(CustomResponseListDataDto<ProductGetDto>.Success(dtosPage,200));
        }

        var products = await _productRepository.GetProductsByNameAdmin(page,name);
        var dtos = products.Select(product => ProductMapper.ToAddDto(product)).ToList();
        
        return CreateActionResult(CustomResponseListDataDto<ProductGetDto>.Success(dtos,200));
        
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetUsersByEmail([FromQuery]int page=1,[FromQuery]string? email ="")
    {

        if (string.IsNullOrEmpty(email))
        {
            var users = await _userRepository.Where(u => u != null)
                .Skip(20 * (page - 1))
                .Take(20)
                .ToListAsync();
            return CreateActionResult(CustomResponseListDataDto<User>.Success(users,ResponseCodes.Ok));

        }
        else
        {            
            var users = await _userRepository.Where(u => u != null && u.Email.Contains(email))
                .Skip(20 * (page - 1))
                .Take(20)
                .ToListAsync();
            return CreateActionResult(CustomResponseListDataDto<User>.Success(users,ResponseCodes.Ok));
            
        }
    }
    
    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {

        var user = await _userRepository.Where(u => u != null && u.Id == id).SingleOrDefaultAsync();

        return CreateActionResult(CustomResponseDto<User>.Success(user, ResponseCodes.Ok));
    }
    
        
    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteUserById(string id)
    {

        var user = await _userRepository.Where(u => u != null && u.Id == id)
            .Include(u=>u.Favorites)
            .Include(u=>u.Locations)
            .Include(u=>u.Orders)
            .SingleOrDefaultAsync();


        foreach (var favorite in user.Favorites)
        {
            favorite.IsDeleted = true;
        }
        
        foreach (var favorite in user.Favorites)
        {
            favorite.IsDeleted = true;
        }

        foreach (var order in user.Orders)
        {
            order.IsDeleted = true;
        }

        var name = user.Email.Split("@")[0];
        var companyEntity = await _companyService.Where(c => c != null && c.Name == name && !c.IsDeleted)
            .SingleOrDefaultAsync();

        if (companyEntity is not null)
        {
            var company = await _companyService.ToggleDeleteCompanyById(companyEntity.Id);

        }
        
        return CreateActionResult(CustomResponseDto<User>.Success(user, ResponseCodes.Ok));
    }

    
}