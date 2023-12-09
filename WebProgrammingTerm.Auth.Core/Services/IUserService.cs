
using WebProgrammingTerm.Auth.Core.DTOs;

namespace WebProgrammingTerm.Auth.Core.Services;

public interface IUserService
{
    Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
    Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
    Task<Response<NoDataDto>> Remove(string id);

    Task<Response<NoDataDto>> AddRoleToUser(string userEmail, string roleName);
}