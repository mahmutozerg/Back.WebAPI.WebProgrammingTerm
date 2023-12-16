
using System.Security.Claims;
using SharedLibrary.DTO;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;

namespace WebProgrammingTerm.Auth.Core.Services;

public interface IUserService
{
    Task<Response<User>> CreateUserAsync(CreateUserDto createUserDto);
    Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
    Task<Response<NoDataDto>> Remove(string id);
    Task<Response<NoDataDto>> AddRoleToUser(string userEmail, string roleName);

    Task<Response<NoDataDto>> UpdateUser(AppUserUpdateDto appUserUpdateDto,ClaimsIdentity claimsIdentity);
    Task<Response<TokenDto>> UpdateUserPassword(UserUpdatePasswordDto userUpdatePasswordDto,ClaimsIdentity claimsIdentity);
}