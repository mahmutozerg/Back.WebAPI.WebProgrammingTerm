using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface ICompanyUserService:IGenericService<CompanyUser>
{
    Task<CustomResponseDto<CompanyUser>> AddAsync(CompanyUserDto companyUserDto,string createdBy);
    Task<CompanyUser> GetCompanyUserWithCompany( string userId);

}