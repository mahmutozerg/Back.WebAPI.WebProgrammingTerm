using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;


namespace WebProgrammingTerm.Core.Services;

public interface ILocationService:IGenericService<Location>
{
    Task<CustomResponseDto<Location>> UpdateAsync(LocationUpdateDto locationUpdateDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<Location>> AddAsync(LocationDto locationDto, ClaimsIdentity claimsIdentity);

    Task<CustomResponseDto<List<Location>?>> GetLocationsAsync(ClaimsIdentity claimsIdentity);
}