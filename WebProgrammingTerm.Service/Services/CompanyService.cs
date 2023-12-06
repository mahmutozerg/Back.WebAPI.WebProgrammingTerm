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

    public  async Task<CustomResponseNoDataDto> AddAsync(CompanyDto companyDto)
    {
        var entityExist = await _companyRepository.Where(c => c.Name == companyDto.Name && !c.IsDeleted).AnyAsync();
        if (entityExist)
            return CustomResponseNoDataDto.Fail(409,ResponseMessages.CompanyNameExist);
            
        
        var entity = CompanyMapper.ToCompany(companyDto);
        await _companyRepository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseNoDataDto.Success(201);
    }

    public  async Task<CustomResponseNoDataDto> UpdateAsync(CompanyDto companyDto)
    {
        var entityExist = await _companyRepository.Where(c => c.Name == companyDto.Name && !c.IsDeleted).AnyAsync();
        if (!entityExist)
            return CustomResponseNoDataDto.Fail(409,ResponseMessages.CompanyNameExist);
            
        
        var entity = CompanyMapper.ToCompany(companyDto);
         _companyRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseNoDataDto.Success(204);
    }
 
}