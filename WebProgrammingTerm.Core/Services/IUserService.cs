using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;


namespace WebProgrammingTerm.Core.Services;

public interface IUserService:IGenericService<User>
{
    Task<CustomResponseDto<User>> AddUserAsync(UserAddDto userAddDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<User>> UpdateUserAsync(AppUserUpdateDto updateDto, ClaimsIdentity claimsIdentity,string accessToken);
    Task<User?> GetUserWithComments(string id);
    Task<User?> GetUserWithFavorites(string id);
    Task<User> GetUserWithLocations(string id);
    Task<User> GetUserWithOrders(string id);
}