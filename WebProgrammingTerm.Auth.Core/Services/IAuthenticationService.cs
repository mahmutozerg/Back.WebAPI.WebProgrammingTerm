using WebProgrammingTerm.Auth.Core.DTOs;

namespace WebProgrammingTerm.Auth.Core.Services;

public interface IAuthenticationService
{
    Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);

    Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

    Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken);

    
}