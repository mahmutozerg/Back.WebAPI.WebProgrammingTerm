using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

public static class CompanyUserMapper
{
    public static CompanyUserDto ToCompanyUserDto(CompanyUser companyUser)
    {
        var companyDto = new CompanyUserDto()
        {
            UserMail = companyUser.Email
        };

        return companyDto;
    }

    public static CompanyUser ToCompanyUser(CompanyUserDto companyUserDto,string id)
    {
        var company = new CompanyUser()
        {
            Id = Guid.NewGuid().ToString(),
            Email = companyUserDto.UserMail,
            CreatedAt = DateTime.Now,
            CreatedBy = id,
            UpdatedAt = DateTime.Now,
            UpdatedBy = id
        };

        return company;
    }
    

}