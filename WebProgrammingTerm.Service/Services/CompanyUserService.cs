﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<CustomResponseDto<CustomResponseNoDataDto>> AddAsync(CompanyUserDto companyUserDto,string createdBy)
    {
        var isCompanyUserExist = await _companyUserRepository.Where(cu =>
            cu.CompanyId == companyUserDto.CompanyId && cu.UserId == companyUserDto.UserId && !cu.IsDeleted).AnyAsync();

        if (isCompanyUserExist)
            return CustomResponseDto<CustomResponseNoDataDto>.Fail(ResponseMessages.CompanyUserAlreadyExist,404);


        var companyEntity = await _companyRepository.Where(c => c.Id == companyUserDto.CompanyId && !c.IsDeleted).FirstOrDefaultAsync();
        
        if(companyEntity is null)
            return CustomResponseDto<CustomResponseNoDataDto>.Fail(ResponseMessages.Notfound,404);
        
        var userEntity = await _userRepository.Where(u => u.Id == companyUserDto.UserId && !u.IsDeleted).FirstOrDefaultAsync();

        if (userEntity is null)
            return CustomResponseDto<CustomResponseNoDataDto>.Fail(ResponseMessages.Notfound,404);


        var companyuser = CompanyUserMapper.ToCompany(companyUserDto, createdBy);
        companyuser.Company = companyEntity;
        companyuser.User = userEntity;
        await _companyUserRepository.AddAsync(companyuser);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<CustomResponseNoDataDto>.Success(201);
    }
}