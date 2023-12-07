using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IDepotService:IGenericService<Depot>
{
    Task<CustomResponseDto<Depot>> UpdateAsync(DepotUpdateDto depotUpdateDto,string updatedBy);


}