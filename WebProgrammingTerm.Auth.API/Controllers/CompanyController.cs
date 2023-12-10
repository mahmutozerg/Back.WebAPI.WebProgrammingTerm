﻿using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Auth.Core.DTO;
 using WebProgrammingTerm.Auth.Core.Services;
 
namespace WebProgrammingTerm.Auth.API.Controllers;

[Authorize(Roles = "Admin,Company" )]

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
    [Authorize(Roles="Company")]
    public async Task<IActionResult> AddUserToCompanyRole(CUserToCompanyRoleDto aUserRoleDto)
    {
        
        var user = await _userService.AddRoleToUser(aUserRoleDto.UserMail,"CompanyUser");
        var userRefreshToken = await _authenticationService.GetUserRefreshTokenByEmail(aUserRoleDto.UserMail);
        var userAccessToken = await _authenticationService.CreateTokenByRefreshToken(userRefreshToken.Token);
        return CreateActionResult(userAccessToken);
    }

    
}