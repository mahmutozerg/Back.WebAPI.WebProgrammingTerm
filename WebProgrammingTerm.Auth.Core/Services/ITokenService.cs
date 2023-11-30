using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;
using WebProgrammingTerm.Auth.Service;

namespace WebProgrammingTerm.Auth.Core.Services;

public interface ITokenService
{
    TokenDto CreateToken(User user);
    ClientTokenDto CreateTokenByClient(Client client);

}