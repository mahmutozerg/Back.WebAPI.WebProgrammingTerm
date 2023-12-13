﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Services;

namespace WebProgrammingTerm.Auth.API.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController:CustomControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authenticationService;

    public AdminController(IUserService userService, IAuthenticationService authenticationService)
    {
        _userService = userService;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserToRole(UserRoleDto userRoleDto)
    {
        var user = await _userService.AddRoleToUser(userRoleDto.UserMail, userRoleDto.RoleName);
        var userRefreshToken = await _authenticationService.GetUserRefreshTokenByEmail(userRoleDto.UserMail);
        var userAccessToken = await _authenticationService.CreateTokenByRefreshToken(userRefreshToken.Token);

        return CreateActionResult(userAccessToken);
    }

}