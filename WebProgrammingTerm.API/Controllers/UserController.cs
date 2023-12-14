using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;
 public class UserController:CustomControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("[action]")]
    [Authorize(Policy = "AdminBypassAuthServerPolicy")]
    public async Task<IActionResult> AddByIdAsync(UserAddDto userAddDto)
    {
 
        return CreateActionResult(await _userService.AddUserAsync(userAddDto,(ClaimsIdentity)User.Identity));
    }

    
    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Update(AppUserUpdateDto userAddDto)
    {
 
        return CreateActionResult(await _userService.UpdateUserAsync(userAddDto,(ClaimsIdentity)User.Identity));
    }
    
    
    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> GetById()
    {
        var claims = (ClaimsIdentity)User.Identity;
        var res = await _userService
            .Where(u => u != null && u.Id == claims.FindFirst(ClaimTypes.NameIdentifier).Value && !u.IsDeleted)
            .SingleOrDefaultAsync();
        if (res is null)
            return CreateActionResult(CustomResponseDto<User>.Fail("User not found", ResponseCodes.BadRequest));

        return CreateActionResult(CustomResponseDto<User>.Success(res,ResponseCodes.Ok));
    }

}