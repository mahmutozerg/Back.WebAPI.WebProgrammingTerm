using WebProgrammingTerm.Auth.Core.Configurations;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;

namespace WebProgrammingTerm.Auth.Core.Services;

public interface ITokenService
{
    Task<TokenDto> CreateTokenAsync(User user);
    ClientTokenDto CreateTokenByClient(Client client);

}