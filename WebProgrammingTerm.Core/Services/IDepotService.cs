using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;


namespace WebProgrammingTerm.Core.Services;

public interface IDepotService:IGenericService<Depot>
{
    Task<CustomResponseDto<Depot>> UpdateAsync(DepotUpdateDto depotUpdateDto,ClaimsIdentity claimsIdentity);


}