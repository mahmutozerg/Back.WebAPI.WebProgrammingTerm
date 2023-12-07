using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;
 
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
        var companyEntity = await _companyRepository.Where(c => c != null && c.Id == companyUpdateDto.TargetId && !c.IsDeleted).SingleOrDefaultAsync();

        if (companyEntity is null)
            throw new Exception(ResponseMessages.CompanyNotFound);
        
        companyEntity.Name = string.IsNullOrWhiteSpace(companyUpdateDto.Name) ? companyEntity.Name : companyUpdateDto.Name;
        companyEntity.Contact = string.IsNullOrWhiteSpace(companyUpdateDto.Contact) ? companyEntity.Contact : companyUpdateDto.Contact;
        companyEntity.UpdatedBy = updatedBy;
        _companyRepository.Update(companyEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Company>.Success(companyEntity,ResponseCodes.Updated);

    }
}