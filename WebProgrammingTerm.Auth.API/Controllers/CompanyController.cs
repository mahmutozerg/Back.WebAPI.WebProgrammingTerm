using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Services;
namespace WebProgrammingTerm.Auth.API.Controllers;

[Authorize(Roles = "Admin,CompanyUser" )]

public class CompanyController:CustomControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authenticationService;

    public CompanyController(IUserService userService, IAuthenticationService authenticationService)
    {
        _userService = userService;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserToCompanyRole(UserToCompanyRoleDto userRoleDto)
    {
        
        var user = await _userService.AddRoleToUser(userRoleDto.UserMail,"CompanyUser");
        var userRefreshToken = await _authenticationService.GetUserRefreshTokenByEmail(userRoleDto.UserMail);
        var userAccessToken = await _authenticationService.CreateTokenByRefreshToken(userRefreshToken.Token);
        return CreateActionResult(userAccessToken);
    }
}