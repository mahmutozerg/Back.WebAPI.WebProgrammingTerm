using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface ICompanyUserService:IGenericService<CompanyUser>
{
    Task<CustomResponseDto<CustomResponseNoDataDto>> AddAsync(CompanyUserDto companyUserDto,string createdBy);

}