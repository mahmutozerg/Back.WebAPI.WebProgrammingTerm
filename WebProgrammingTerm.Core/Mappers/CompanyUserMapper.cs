using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Mappers;

public static class CompanyUserMapper
{
    public static CompanyUserDto ToCompanyUserDto(CompanyUser companyUser)
    {
        var companyDto = new CompanyUserDto()
        {
            CompanyId = companyUser.Id,
            UserMail = companyUser.UserMail
        };

        return companyDto;
    }

    public static CompanyUser ToCompany(CompanyUserDto companyUserDto,string id)
    {
        var company = new CompanyUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserMail = companyUserDto.UserMail,
            CompanyId = companyUserDto.CompanyId,
            CreatedAt = DateTime.Now,
            CreatedBy = id,
            UpdatedAt = DateTime.Now,
            UpdatedBy = id
        };

        return company;
    }
    

}