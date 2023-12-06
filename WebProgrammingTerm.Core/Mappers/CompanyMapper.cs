using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Mappers;

public static class CompanyMapper
{
    public static CompanyDto ToCompanyDto(Company company)
    {
        var companyDto = new CompanyDto()
        {
            Contact = company.Contact,
            Name = company.Name
        };

        return companyDto;
    }

    public static Company ToCompany(CompanyDto companyDto,string id)
    {
        var company = new Company()
        {
            Id = Guid.NewGuid().ToString(),
            Contact = companyDto.Contact,
            Name = companyDto.Name,
            CreatedAt = DateTime.Now,
            CreatedBy = id,
            UpdatedAt = DateTime.Now

         };

        return company;
    }
    
    public static Company ToCompany(CompanyUpdateDto companyDto,string id)
    {
        var company = new Company()
        {
            Id = companyDto.TargetId,
            Contact = companyDto.Contact,
            Name = companyDto.Name,
            CreatedAt = DateTime.Now,
            CreatedBy = "System",
            UpdatedBy = id,
            UpdatedAt = DateTime.Now
        };

        return company;
    }
}