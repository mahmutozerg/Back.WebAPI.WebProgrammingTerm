using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SharedLibrary.DTO;
using SharedLibrary.Mappers;
using SharedLibrary.Models;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using IUnitOfWork = WebProgrammingTerm.Core.UnitOfWorks.IUnitOfWork;

namespace WebProgrammingTerm.Service.Services;

public class CompanyService:GenericService<Company>,ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly List<ClientTokenDto> _clientTokenOptions;
    public CompanyService(IUnitOfWork unitOfWork, ICompanyRepository companyRepository, IOptions<List<ClientTokenDto>> tokenOptions) : base(companyRepository,unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
        _clientTokenOptions = tokenOptions.Value;
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

    public async Task<CustomResponseDto<CompanyUserDto>> AddAsync(CompanyAddDto companyAddDto, string createdBy,string token)
    {
        var companyEntity = await _companyRepository.Where(c => c != null && c.Name == companyAddDto.Name&& !c.IsDeleted).SingleOrDefaultAsync();
        
        if (companyEntity is not null)
            throw new Exception(ResponseMessages.CompanyExist);

        companyEntity = CompanyMapper.ToCompany(companyAddDto);

        
        using (var client = new HttpClient())
        {
            var url = "https://localhost:7049/api";
            var createUser = "/User/CreateUser";
            var addrole = "/Admin/AddUserToRole";
             var createUserRequestData = new
            {
                email = companyEntity.Name.Normalize().Replace(" ","")+"@example.com",
                password = "!"+companyEntity.Name[0].ToString().Normalize().Replace(" ","").ToLowerInvariant()+companyEntity.Name[1..].Normalize().Replace(" ","").ToUpperInvariant()+"0"
            };
            
            var createUserJsonData = JsonConvert.SerializeObject(createUserRequestData);

            var content = new StringContent(createUserJsonData, Encoding.UTF8, "application/json");
            
            
            var response = await client.PostAsync(url+createUser,content);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ToString());

            var userRoleRequestData = new
            {
                userMail = createUserRequestData.email,
                roleName = "Company"
            };
            var  userRoleJsonData = JsonConvert.SerializeObject(userRoleRequestData);
            content = new StringContent(userRoleJsonData, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            response = await client.PostAsync(url+addrole,content);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ToString());
            
            await base.AddAsync(companyEntity, createdBy);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<CompanyUserDto>.Success(new CompanyUserDto()
            {
                UserMail = createUserRequestData.email,
                
            }, 200);
        }
        throw new Exception("Authserver cannot reach api  ");

    }
}