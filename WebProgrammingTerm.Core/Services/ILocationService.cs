using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface ILocationService:IGenericService<Location>
{
    Task<CustomResponseDto<Location>> UpdateAsync(LocationUpdateDto locationUpdateDto,string updatedBy);
    Task<CustomResponseDto<Location>> AddAsync(LocationDto locationDto, string createdBy);
}