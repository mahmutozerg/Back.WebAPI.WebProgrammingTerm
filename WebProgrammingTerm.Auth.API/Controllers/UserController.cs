using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;
using WebProgrammingTerm.Auth.Core.Services;

namespace WebProgrammingTerm.Auth.API.Controllers;

public class UserController:CustomControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authenticationService;
     public UserController(IUserService userService, IGenericService<User> genericService, IAuthenticationService authenticationService)
     {
         _userService = userService;
         _authenticationService = authenticationService;
     }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        var result = await _userService.CreateUserAsync(createUserDto);
        if (result.StatusCode != 200) 
            return CreateActionResult(result);
        var loginD = new LoginDto()
        {
            Email = createUserDto.Email,
            Password = createUserDto.Password
        };
        var token = await _authenticationService.CreateTokenAsync(loginD);
        return CreateActionResult(  token);

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
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateUser(AppUserUpdateDto userUpdateDto)
    {
        var user = await _userService.UpdateUser(userUpdateDto,(ClaimsIdentity)User.Identity);

        return CreateActionResult(user);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateUserPassword(UserUpdatePasswordDto userUpdateDto)
    {
        var user = await _userService.UpdateUserPassword(userUpdateDto,(ClaimsIdentity)User.Identity);

        return CreateActionResult(user);
    }

}