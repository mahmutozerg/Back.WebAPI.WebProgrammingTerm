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
    public async Task<IActionResult> AddByIdAsync(string id)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;

        return CreateActionResult(await _userService.AddUserByIdAsync(id,claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }

}