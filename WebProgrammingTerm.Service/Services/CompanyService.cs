using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;
using WebProgrammingTerm.Core.Mappers;

namespace WebProgrammingTerm.Service.Services;

public class CompanyService:GenericService<Company>,ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CompanyService(IUnitOfWork unitOfWork, ICompanyRepository companyRepository) : base(companyRepository,unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
    }

    public  async Task<CustomResponseDto<Company>> UpdateAsync(CompanyUpdateDto companyUpdateDto,string updatedBy)
    {
        var entity = await _companyRepository.Where(c => c != null && c.Id == companyUpdateDto.TargetId && !c.IsDeleted).SingleOrDefaultAsync();

        if (entity is null)
            throw new Exception(ResponseMessages.CompanyNotFound);
        
        entity.Name = string.IsNullOrWhiteSpace(companyUpdateDto.Name) ? entity.Name : companyUpdateDto.Name;
        entity.Contact = string.IsNullOrWhiteSpace(companyUpdateDto.Contact) ? entity.Contact : companyUpdateDto.Contact;
        entity.UpdatedBy = updatedBy;
        _companyRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Company>.Success(entity,ResponseCodes.Updated);

    }
}