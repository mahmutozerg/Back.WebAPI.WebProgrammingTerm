using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;
[Authorize]

public class UserFavoriteController:CustomControllerBase
{
    private readonly IUserFavoriteService _userFavoriteService;

    public UserFavoriteController(IUserFavoriteService  userFavoriteService)
    {
        _userFavoriteService = userFavoriteService;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Add( UserFavoritesDto userFavoritesDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _userFavoriteService.AddAsync(userFavoritesDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(UserFavorites userFavoritesDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _userFavoriteService.UpdateAsync(userFavoritesDto,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
}