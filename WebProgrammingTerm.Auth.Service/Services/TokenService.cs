using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;
using WebProgrammingTerm.Auth.Core.Services;
using WebProgrammingTerm.Auth.Service.Configurations;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebProgrammingTerm.Auth.Service.Services;

public class TokenService:ITokenService
{
    
    /// <summary>
    ///  user manageri sildim haberin olsun;
    /// </summary>
    private readonly AppTokenOptions _tokenOptions;

    public TokenService(IOptions<AppTokenOptions> tokenOptions)
    {
        _tokenOptions = tokenOptions.Value;
    }
   
    
    public TokenDto CreateToken(User user)
    {
        var accesTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
        var securityKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var jwtSecurityToken = new JwtSecurityToken(

            issuer: _tokenOptions.Issuer,
            expires: accesTokenExpiration,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials,
            claims: GetClaims(user, _tokenOptions.Audience)
        );

        var jwtHandler = new JwtSecurityTokenHandler();
        var token = jwtHandler.WriteToken(jwtSecurityToken);

        var tokendto = new TokenDto
        {
            AccesssToken = token,
            AccesTokenExpiration = accesTokenExpiration,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return tokendto;
    }

    
    private string CreateRefreshToken()
    {
        var numberByte = new Byte[64];

        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);

        return Convert.ToBase64String(numberByte);
    }

    private IEnumerable<Claim> GetClaims(User user , List<String> aud)
    {
        var userClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        userClaims.AddRange(aud.Select(c => new Claim(JwtRegisteredClaimNames.Aud, c)));
        return userClaims;
    }



}