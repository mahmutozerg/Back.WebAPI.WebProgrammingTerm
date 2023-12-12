using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;


namespace WebProgrammingTerm.Core.Services;

public interface ICompanyUserService:IGenericService<CompanyUser>
{
    Task<CustomResponseDto<CompanyUser>> AddAsync(CompanyUserDto companyUserDto,ClaimsIdentity claimsIdentity,string accessToken );
    Task<CompanyUser> GetCompanyUserWithCompany( string userId);

}