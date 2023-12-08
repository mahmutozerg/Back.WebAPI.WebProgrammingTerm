using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IUserService:IGenericService<User>
{
    Task<CustomResponseDto<User>> AddUserByIdAsync(string id,string createdBy);
    Task<User> GetUserWithComments(string id);
    Task<User> GetUserWithFavorites(string id);
    Task<User> GetUserWithLocations(string id);
}