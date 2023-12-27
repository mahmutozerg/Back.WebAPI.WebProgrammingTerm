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
using WebProgrammingTerm.Core.UnitOfWorks;

namespace WebProgrammingTerm.Service.Services;

public class UserService:GenericService<User>,IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserFavoritesRepository _favoritesRepository;
    private readonly ICompanyService _companyService;
    private const string UpdateUserUrl = "https://localhost:7049/api/User/UpdateUser";
    public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserFavoritesRepository favoritesRepository, ICompanyService companyService) : base(userRepository,unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _favoritesRepository = favoritesRepository;
        _companyService = companyService;
    }

    public async Task<CustomResponseDto<User>> AddUserAsync(UserAddDto userAddDto,ClaimsIdentity claimsIdentity)
    {

        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userExist = await _userRepository.AnyAsync(u => u != null && u.Email == userAddDto.Email);
        if (userExist)
            throw new Exception(ResponseMessages.UserAlreadyExist);

        var user = AppUserMapper.ToUser(userAddDto);
        user.CreatedBy = createdBy;
        user.UpdatedBy = createdBy;
        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<User>.Success(user,ResponseCodes.Created);

    }

    private static async Task SendUpdateReqToAuthAsync(AppUserUpdateDto updateDto,string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(JsonConvert.SerializeObject(updateDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(UpdateUserUrl,content);

            if (response is null)
                throw new Exception(ResponseMessages.UserNotFound);
            
        }
    }
    public async Task<CustomResponseDto<User>> UpdateUserAsync(AppUserUpdateDto updateDto, ClaimsIdentity claimsIdentity,string accessToken)
    {
        var userId = "";
        if (string.IsNullOrEmpty(updateDto.Id))
        {
            userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        }
        else
        {
             userId = updateDto.Id;
        }
        var isCompany = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
      

        var userEntity = await _userRepository.Where(u => u != null && u.Id == userId && !u.IsDeleted).SingleOrDefaultAsync();
        
        if (userEntity is null)
            return CustomResponseDto<User>.Fail(ResponseMessages.UserNotFound, ResponseCodes.NotFound);

    
        if (updateDto.Email != userEntity.Email)
        {
            var emailExist = await _userRepository.Where(u => u != null && u.Email == updateDto.Email && !u.IsDeleted).AnyAsync();
            
            if (emailExist)
                return CustomResponseDto<User>.Fail(ResponseMessages.UserMailExist, ResponseCodes.Duplicate);

            if (isCompany is null or "Admin")
            {
                var name = userEntity.Email.Split("@")[0];
                var  companyEntity = await _companyService.Where(c => c != null && c.Name == name && !c.IsDeleted).SingleOrDefaultAsync();
                if (companyEntity is not null )
                {
                    var company = await _companyService.GetCompanyByName(userEntity.Email.Split("@")[0]);
                    company.Name = updateDto.Email.Split("@")[0];
                    await _companyService.UpdateAsync(company, userId);

                }
                
            }

        }
        
        var tempData = userEntity.Email;
        userEntity = AppUserMapper.UpdateUser(userEntity, updateDto);

        //checks if user updated its email if yes we should also update it in authserver
        if (userEntity.Email != tempData)
        {
            await SendUpdateReqToAuthAsync(updateDto, accessToken);
        }
        userEntity.UpdatedBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _userRepository.Update(userEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<User>.Success(userEntity, ResponseCodes.Updated);
    }

    public async Task<User?> GetUserWithComments(string id)
    {
        var user= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u=>u!.Comments)
            .SingleOrDefaultAsync();

        if (user is null)
            new Exception(ResponseMessages.UserNotFound);

        return user;
    }

    public async Task<User?> GetUserWithFavorites(string id)
    {
        var user= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u => u!.Favorites.OrderBy(f => f.UpdatedAt))
            .SingleOrDefaultAsync();

        if (user is null)
            new Exception(ResponseMessages.UserNotFound);
        
        

        return user;
        
    }

    public async Task<User> GetUserWithLocations(string id)
    {
       var userEntity= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u=>u.Locations)
            .FirstOrDefaultAsync();
        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);


        return userEntity;
    }

    public async Task<User> GetUserWithOrders(string id)
    {
        var userEntity= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u=>u!.Orders)
            .FirstOrDefaultAsync();
        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);


        return userEntity;    
        
    }
    public async Task<List<UserFavorites?>> GetFavorites(string userId)
    {
        var userEntity = await _favoritesRepository
            .Where(f => f != null  && f.UserId == userId && !f.IsDeleted)
            .OrderByDescending(f=>f.UpdatedAt)
            .ToListAsync();


        return userEntity;

    }
    
}