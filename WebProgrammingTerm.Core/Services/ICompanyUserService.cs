using System.Security.Claims;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface ICompanyUserService:IGenericService<CompanyUser>
{
    Task<CustomResponseDto<CompanyUser>> AddAsync(CompanyUserDto companyUserDto,ClaimsIdentity claimsIdentity,string accessToken );
    Task<CompanyUser> GetCompanyUserWithCompany( string userId);

}