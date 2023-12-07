using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IDepotService:IGenericService<Depot>
{
    Task<CustomResponseNoDataDto> UpdateAsync(DepotUpdateDto depotUpdateDto,string updatedBy);
    Task<CustomResponseNoDataDto> AddAsync(DepotAddDto depotUpdateDto, string createdBy);


}