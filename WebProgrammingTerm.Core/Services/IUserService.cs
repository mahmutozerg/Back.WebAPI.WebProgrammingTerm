using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;


namespace WebProgrammingTerm.Core.Services;

public interface IUserService:IGenericService<AppUser>
{
    Task<CustomResponseDto<AppUser>> AddUserAsync(UserAddDto userAddDto,ClaimsIdentity claimsIdentity);
    Task<AppUser> GetUserWithComments(string id);
    Task<AppUser> GetUserWithFavorites(string id);
    Task<AppUser> GetUserWithLocations(string id);
    Task<AppUser> GetUserWithOrders(string id);
}