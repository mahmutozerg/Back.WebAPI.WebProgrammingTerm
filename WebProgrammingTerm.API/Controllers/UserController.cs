using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;
[Authorize(Policy = "AdminBypassAuthServerPolicy")]
 public class UserController:CustomControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddByIdAsync(UserAddDto userAddDto)
    {
 
        return CreateActionResult(await _userService.AddUserAsync(userAddDto,(ClaimsIdentity)User.Identity));
    }

}