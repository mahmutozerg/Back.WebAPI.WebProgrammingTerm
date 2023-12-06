using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;

namespace WebProgrammingTerm.Auth.Core.Services;

public interface ITokenService
{
    TokenDto CreateToken(User user);

}