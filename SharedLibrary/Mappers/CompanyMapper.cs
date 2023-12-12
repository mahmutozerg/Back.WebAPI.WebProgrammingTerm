using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

public static class CompanyMapper
{
    public static CompanyAddDto ToCompanyDto(Company company)
    {
        var companyDto = new CompanyAddDto()
        {
            Contact = company.Contact,
            Name = company.Name
        };

        return companyDto;
    }

    public static Company ToCompany(CompanyAddDto companyAddDto )
    {
        var company = new Company()
        {
            Id = Guid.NewGuid().ToString(),
            Contact = companyAddDto.Contact,
            Name = companyAddDto.Name,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            
         };

        return company;
    }
    
    public static Company ToCompany(CompanyUpdateDto companyDto )
    {
        var company = new Company()
        {
            Id = companyDto.TargetId,
            Contact = companyDto.Contact,
            Name = companyDto.Name,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        return company;
    }
}