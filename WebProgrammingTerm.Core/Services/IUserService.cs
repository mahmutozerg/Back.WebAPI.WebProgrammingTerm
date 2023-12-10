using System.Security.Claims;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IUserService:IGenericService<User>
{
    Task<CustomResponseDto<User>> AddUserAsync(UserAddDto userAddDto,ClaimsIdentity claimsIdentity);
    Task<User> GetUserWithComments(string id);
    Task<User> GetUserWithFavorites(string id);
    Task<User> GetUserWithLocations(string id);
    Task<User> GetUserWithOrders(string id);
}