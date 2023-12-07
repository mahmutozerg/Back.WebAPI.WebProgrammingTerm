using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IUserService:IGenericService<User>
{
    Task<CustomResponseDto<CustomResponseNoDataDto>> AddUserByIdAsync(string id,string createdBy);
}