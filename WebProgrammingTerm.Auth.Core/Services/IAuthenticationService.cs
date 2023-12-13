using SharedLibrary.DTO;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;

namespace WebProgrammingTerm.Auth.Core.Services;

public interface IAuthenticationService
{
    Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);

    Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

    Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken);

    Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);
    Task<Response<NoDataDto>> AddRole(string role);

    Task<UserRefreshToken> GetUserRefreshTokenByEmail(string userEmail);


}