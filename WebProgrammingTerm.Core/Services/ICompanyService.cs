using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface ICompanyService:IGenericService<Company>
{
    Task<CustomResponseDto<Company>> UpdateAsync(CompanyUpdateDto companyUpdateDtoDto,string updatedBy);
}