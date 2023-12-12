using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedLibrary.DTO;
using SharedLibrary.Mappers;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;

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

    public async Task<CustomResponseDto<CompanyUser>> AddAsync(CompanyUserDto companyUserDto,ClaimsIdentity claimsIdentity,string accessToken)
    {
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var companyName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        // this is a company not a company user. Company are only stored in user table and company table not in companyuser table
        // Our system's rule company's name are unique
        var userEntity = await _userRepository.Where(u => u.Email == companyUserDto.UserMail && !u.IsDeleted).FirstOrDefaultAsync();
        
        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);
        
        var isCompanyUserExist = await _companyUserRepository.Where(cu =>
            cu.Email == companyUserDto.UserMail && !cu.IsDeleted).AnyAsync();

        if (isCompanyUserExist)
            throw new Exception(ResponseMessages.CompanyUserAlreadyExist);


        var companyEntity =
            await _companyRepository.Where(c => c.Name == companyName  && !c.IsDeleted).SingleOrDefaultAsync();

        if (companyEntity is null)
            throw new Exception(ResponseMessages.CompanyNotFound);


        var companyUser = CompanyUserMapper.ToCompany(companyUserDto, createdBy);
        companyUser.Company = companyEntity;
        companyUser.CompanyId = companyEntity.Id;
        companyUser.User = userEntity;
        
        

        using (var client = new HttpClient())
        { 
            string url = "https://localhost:7049/api/Company/AddUserToCompanyRole";
            
            var requestData = new
            {
                companyId = companyUser.CompanyId,
                userMail = companyUser.Email
                
            };

            string jsonData = JsonConvert.SerializeObject(requestData);

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

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