using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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


    public AuthenticationService(ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> refreshTokenService)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _refreshTokenService = refreshTokenService;
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

        var userRefreshToken = await _refreshTokenService.Where(r => r.UserId == user.Id).SingleOrDefaultAsync();

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
        var refreshToken = await _refreshTokenService.Where(r => r.Token == _refreshToken).SingleOrDefaultAsync();

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