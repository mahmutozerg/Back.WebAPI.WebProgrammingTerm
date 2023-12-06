using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebProgrammingTerm.Auth.Core.Configurations;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;
using WebProgrammingTerm.Auth.Core.Services;
using WebProgrammingTerm.Auth.Service.Configurations;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebProgrammingTerm.Auth.Service.Services;

public class TokenService:ITokenService
{
    private readonly UserManager<User> _UserManager;
    private readonly AppTokenOptions _tokenOptions;
    private readonly ClientLoginDto _clientTokenOptions;

    public TokenService(UserManager<User> userManager, IOptions<AppTokenOptions> tokenOptions, IOptions<ClientLoginDto>  clientTokenOptions)
    {
        _UserManager = userManager;
        _tokenOptions = tokenOptions.Value;
        _clientTokenOptions = clientTokenOptions.Value;
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

    public ClientTokenDto CreateTokenByClient(Client client)
    {
        var accesTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var jwtSecurityToken = new JwtSecurityToken(
            
            issuer: _tokenOptions.Issuer,
            expires: accesTokenExpiration,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials,
            claims: GetClaimsByClient(client)
        );

        var jwtHandler = new JwtSecurityTokenHandler();
        var token = jwtHandler.WriteToken(jwtSecurityToken);

        var tokendto = new ClientTokenDto
        {
            AccesToken = token,
            AccesTokenExpiration = accesTokenExpiration
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

    private IEnumerable<Claim> GetClaimsByClient(Client client)
    {
        var claims = new List<Claim>();
        claims.AddRange(client.Audiences.Select(a=> new Claim(JwtRegisteredClaimNames.Aud,a)));

        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.NameId, client.Id.ToString()));
        
        return claims;
    }

}