using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Services;

namespace WebProgrammingTerm.Auth.API.Controllers;

public class AuthController:CustomControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    
    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateTokenByClient(ClientLoginDto clientLoginDto)
    {
        var result =  _authenticationService.CreateTokenByClient(clientLoginDto);

        return CreateActionResult(result);
    }



    [HttpPost]
    public async Task<IActionResult> CreateToken(LoginDto loginDto)
    {
        var result = await _authenticationService.CreateTokenAsync(loginDto);
        return CreateActionResult(result);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> RevokeRefreshToken(string refreshToken)
    {
        var result = await _authenticationService.RevokeRefreshToken(refreshToken);
        return CreateActionResult(result);

    }

    [HttpPost]
    public async Task<IActionResult> CreateTokenByRefreshToken(string refreshToken)
    {
        var result = await _authenticationService.CreateTokenByRefreshToken(refreshToken);
        return CreateActionResult(result);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
     public async Task<IActionResult> AddRole(string role)
    {
        var result = await _authenticationService.AddRole(role);

        return CreateActionResult(result);
    }
     
     
     [HttpGet]
     [Authorize]
     public async Task<IActionResult> GetRole()
     {
         var claimsIdentity = (ClaimsIdentity)User.Identity;
         var role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
         
         
         return CreateActionResult(Response<string?>.Success(role,200));
     }
}