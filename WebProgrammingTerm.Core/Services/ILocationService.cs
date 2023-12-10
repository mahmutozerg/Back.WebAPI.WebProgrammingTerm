using System.Security.Claims;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface ILocationService:IGenericService<Location>
{
    Task<CustomResponseDto<Location>> UpdateAsync(LocationUpdateDto locationUpdateDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<Location>> AddAsync(LocationDto locationDto, ClaimsIdentity claimsIdentity);
}