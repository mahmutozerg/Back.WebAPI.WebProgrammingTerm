using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface ICompanyUserService:IGenericService<CompanyUser>
{
    Task<CustomResponseDto<TokenDto>> AddAsync(CompanyUserDto companyUserDto,string createdBy,string token);
    Task<CompanyUser> GetCompanyUserWithCompany( string userId);

}