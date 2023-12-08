using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebProgrammingTerm.Auth.Core.Configurations;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;
using WebProgrammingTerm.Auth.Core.Repositories;
using WebProgrammingTerm.Auth.Core.Services;

namespace WebProgrammingTerm.Auth.Service.Services;

public class AuthenticationService:IAuthenticationService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<UserRefreshToken> _refreshTokenService;
    private readonly List<ClientLoginDto >_tokenOptions;


    public AuthenticationService(ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> refreshTokenService, IOptions<List<ClientLoginDto>> tokenOptions)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _refreshTokenService = refreshTokenService;
        _tokenOptions = tokenOptions.Value;
    }

    public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
    {
        if (loginDto is null)
            throw new ArgumentNullException(nameof(loginDto));

        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
            return Response<TokenDto>.Fail("Email or password is wrong", 400,true);

        if (! await _userManager.CheckPasswordAsync(user,loginDto.Password))
            return Response<TokenDto>.Fail("Email or password is wrong", 400,true);

        var token = _tokenService.CreateToken(user);

        var userRefreshToken = await _refreshTokenService.Where(r => r != null && r.UserId == user.Id).SingleOrDefaultAsync();

        if (userRefreshToken is null)
        {
            await _refreshTokenService.AddAsync(new()
                { UserId = user.Id, Token = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
        }
        else
        {
            userRefreshToken.Token = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        }

        await _unitOfWork.CommitAsync();
        return Response<TokenDto>.Success(token,200);
    }

    public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string _refreshToken)
    {
        
        var refreshToken = await _refreshTokenService.Where(r => r != null && r.Token == _refreshToken).SingleOrDefaultAsync();

        if (refreshToken is null)
            return Response<TokenDto>.Fail("Refresh token does not exist", 404,true);

        var user = await _userManager.FindByIdAsync(refreshToken.UserId);
        if (user is null)
            return Response<TokenDto>.Fail("User does not exist", 404,true);

        var token = _tokenService.CreateToken(user);
        refreshToken.Token = token.RefreshToken;
        refreshToken.Expiration = token.RefreshTokenExpiration;

        await _unitOfWork.CommitAsync();
        return Response<TokenDto>.Success(token, 200);

        
    }
    public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
    {
         if (_tokenOptions != null && _tokenOptions.Any())
        {
            foreach (var configuredClient in _tokenOptions)
            {
                if (string.CompareOrdinal(configuredClient.Id, clientLoginDto.Id) == 0
                    && string.CompareOrdinal(configuredClient.Secret, clientLoginDto.Secret) == 0)
                {
                    var client = new Client()
                    {
                        Id = clientLoginDto.Id,
                        Secret = clientLoginDto.Secret,
                        Audiences = new List<string> { "www.bookerr.com" }
                    };

                    var clientTokenDto = _tokenService.CreateTokenByClient(client);
                    return Response<ClientTokenDto>.Success(statusCode: 200, data: clientTokenDto);
                }
            }
        }

        throw new Exception("Client Does not Exist");
    }
    
    public async Task<Response<NoDataDto>> RevokeRefreshToken(string _refreshToken)
    {
        var refreshToken = await _refreshTokenService.Where(r => r.Token == _refreshToken).FirstOrDefaultAsync();
        if (refreshToken is null)
            return Response<NoDataDto>.Fail("Refresh token does not exist", 404,true);
        
        _refreshTokenService.Remove(refreshToken);
        await _unitOfWork.CommitAsync();


        return Response<NoDataDto>.Success(200);

    }

    
}