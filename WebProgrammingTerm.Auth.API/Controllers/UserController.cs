using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;
using WebProgrammingTerm.Auth.Core.Services;

namespace WebProgrammingTerm.Auth.API.Controllers;

public class UserController:CustomControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
     public UserController(IUserService userService, IGenericService<User> genericService, ITokenService tokenService)
     {
         _userService = userService;
         _tokenService = tokenService;
     }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        var result = await _userService.CreateUserAsync(createUserDto);
        if (result.StatusCode == 200)
        {
            var token = await _tokenService.CreateTokenAsync(result.Data);
            return CreateActionResult(  Response<TokenDto>.Success(token,200));

        }        
        
        return CreateActionResult(result);
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUser()
    {
        var a = HttpContext.User.Identity;
        var user = await _userService.GetUserByNameAsync(User.Identity.Name);

        return CreateActionResult(user);
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUser()
    {
        var user = await _userService.Remove(User.FindFirstValue(ClaimTypes.NameIdentifier));

        return CreateActionResult(user);
    }
    
  

}