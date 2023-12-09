using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using IUnitOfWork = WebProgrammingTerm.Core.UnitOfWorks.IUnitOfWork;

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

    public async Task<CustomResponseDto<CompanyUser>> AddAsync(CompanyUserDto companyUserDto,string createdBy,string token)
    {
        var isCompanyUserExist = await _companyUserRepository.Where(cu =>
            cu.UserMail == companyUserDto.UserMail && !cu.IsDeleted).AnyAsync();

        if (isCompanyUserExist)
            throw new Exception(ResponseMessages.CompanyUserAlreadyExist);

        var existingCompanyUser = await _companyUserRepository
            .Where(cu => cu.UserId == createdBy)
            .Include(cu => cu.Company).FirstOrDefaultAsync();

        if (existingCompanyUser is null)
            throw new Exception(ResponseMessages.CompanyNotFound);

        
        var userEntity = await _userRepository.Where(u => u.MailAddress == companyUserDto.UserMail && !u.IsDeleted).FirstOrDefaultAsync();

        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);
        

        var companyUser = CompanyUserMapper.ToCompany(companyUserDto, createdBy);
        companyUser.Company = existingCompanyUser.Company;
        companyUser.CompanyId = existingCompanyUser.Company.Id;
        companyUser.User = userEntity;
        


        using (var client = new HttpClient())
        { 
            string url = "https://localhost:7049/api/Company/AddUserToCompanyRole";
            
            var requestData = new
            {
                companyId = companyUser.CompanyId,
                userMail = companyUser.UserMail
                
            };

             string jsonData = JsonConvert.SerializeObject(requestData);

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync(url,content);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong");

            await _companyUserRepository.AddAsync(companyUser);
            await _unitOfWork.CommitAsync();

        }
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