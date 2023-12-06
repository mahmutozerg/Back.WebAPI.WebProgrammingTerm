using WebProgrammingTerm.Auth.Core.Configurations;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;

namespace WebProgrammingTerm.Auth.Core.Services;

public interface ITokenService
{
    TokenDto CreateToken(User user);
    ClientTokenDto CreateTokenByClient(Client client);

}