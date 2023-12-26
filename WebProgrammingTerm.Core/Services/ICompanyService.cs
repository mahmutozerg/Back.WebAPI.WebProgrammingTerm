

using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace WebProgrammingTerm.Core.Services;

public interface ICompanyService:IGenericService<Company>
{
    Task<CustomResponseDto<Company>> UpdateAsync(CompanyUpdateDto companyUpdateDtoDto,string updatedBy);
    Task<Company?> GetCompanyByName(string name);
    Task<CustomResponseNoDataDto> ToggleDeleteCompanyById(string id);
    Task<CustomResponseDto<CompanyUserDto>> AddAsync(CompanyAddDto companyAddDto,string createdBy,string accessToken);

}