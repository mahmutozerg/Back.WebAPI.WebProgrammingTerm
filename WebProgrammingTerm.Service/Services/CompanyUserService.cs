using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;

namespace WebProgrammingTerm.Service.Services;

public class CompanyUserService:GenericService<CompanyUser>,ICompanyUserService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyUserRepository _companyUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CompanyUserService(IUnitOfWork unitOfWork, ICompanyUserRepository companyUserRepository, ICompanyRepository companyRepository, IUserRepository userRepository) : base(companyUserRepository,unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _companyUserRepository = companyUserRepository;
        _companyRepository = companyRepository;
        _userRepository = userRepository;
    }

    public async Task<CustomResponseDto<CompanyUser>> AddAsync(CompanyUserDto companyUserDto,string createdBy)
    {
        var isCompanyUserExist = await _companyUserRepository.Where(cu =>
            cu.CompanyId == companyUserDto.CompanyId && cu.UserId == companyUserDto.UserId && !cu.IsDeleted).AnyAsync();

        if (isCompanyUserExist)
            throw new Exception(ResponseMessages.CompanyUserAlreadyExist);

        var companyEntity = await _companyRepository.Where(c => c.Id == companyUserDto.CompanyId && !c.IsDeleted).FirstOrDefaultAsync();

        if (companyEntity is null)
            throw new Exception(ResponseMessages.CompanyNotFound);

        
        var userEntity = await _userRepository.Where(u => u.Id == companyUserDto.UserId && !u.IsDeleted).FirstOrDefaultAsync();

        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);
        

        var companyUser = CompanyUserMapper.ToCompany(companyUserDto, createdBy);
        companyUser.Company = companyEntity;
        companyUser.User = userEntity;
        await _companyUserRepository.AddAsync(companyUser);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<CompanyUser>.Success(companyUser,ResponseCodes.Created);
    }

    public async Task<CompanyUser> GetCompanyUserWithCompany( string userId)
    {
        var companyUserEntity =
            await _companyUserRepository
                .Where(cu => cu != null  && cu.UserId == userId && !cu.IsDeleted)
                .Include(cu=>cu.Company)
                .FirstOrDefaultAsync();
        
        if (companyUserEntity is null)
            throw new Exception(ResponseMessages.CompanyUserNotFound);

        return companyUserEntity;
    }
}