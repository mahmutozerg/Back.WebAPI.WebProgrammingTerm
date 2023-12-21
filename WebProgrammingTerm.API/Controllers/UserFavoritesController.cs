using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;
[Authorize]

public class UserFavoritesController:CustomControllerBase
{
    private readonly IUserFavoriteService _userFavoriteService;

    public UserFavoritesController(IUserFavoriteService  userFavoriteService)
    {
        _userFavoriteService = userFavoriteService;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Add( UserFavoritesDto userFavoritesDto)
    {
        return CreateActionResult(await _userFavoriteService.AddAsync(userFavoritesDto,(ClaimsIdentity)User.Identity));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(UserFavoritesDto userFavoritesDto)
    {
         return CreateActionResult(await _userFavoriteService.UpdateAsync(userFavoritesDto,(ClaimsIdentity)User.Identity));
    }
    
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetFavorites()
    {
        return CreateActionResult(await _userFavoriteService.GetAsync((ClaimsIdentity)User.Identity));
    }
}