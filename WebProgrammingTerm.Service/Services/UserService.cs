using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
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

    public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository) : base(userRepository,unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
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

    public async Task<CustomResponseDto<User>> UpdateUserAsync(AppUserUpdateDto updateDto, ClaimsIdentity claimsIdentity)
    {
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userEntity = await _userRepository.Where(u => u != null && u.Id == userId && !u.IsDeleted).SingleOrDefaultAsync();

        var emailExist = await _userRepository.Where(u => u != null && u.Email == updateDto.Email && !u.IsDeleted).AnyAsync();
        if (userEntity is null)
            return CustomResponseDto<User>.Fail(ResponseMessages.UserNotFound, ResponseCodes.NotFound);
        if (emailExist)
            return CustomResponseDto<User>.Fail(ResponseMessages.UserMailExist, ResponseCodes.Duplicate);
        
        userEntity.Email =  string.IsNullOrWhiteSpace(updateDto.Email) ? userEntity.Email : updateDto.Email;
        userEntity.Name =  string.IsNullOrWhiteSpace(updateDto.Name) ? userEntity.Name : updateDto.Name;
        userEntity.LastName =  string.IsNullOrWhiteSpace(updateDto.LastName) ? userEntity.LastName : updateDto.LastName;
        userEntity.BirthDate =  string.IsNullOrWhiteSpace(updateDto.BirthDate) ? userEntity.BirthDate : updateDto.BirthDate;
        userEntity.Age = updateDto.Age == int.MinValue ? userEntity.Age : updateDto.Age;


        _userRepository.Update(userEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<User>.Success(userEntity, ResponseCodes.Updated);
    }

    public async Task<User> GetUserWithComments(string id)
    {
        var user= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u=>u!.Comments)
            .SingleOrDefaultAsync();

        if (user is null)
            new Exception(ResponseMessages.UserNotFound);

        return user;
    }

    public async Task<User> GetUserWithFavorites(string id)
    {
        var user= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u=>u!.Favorites)
            .SingleOrDefaultAsync();

        if (user is null)
            new Exception(ResponseMessages.UserNotFound);

        return user;    }

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
            .Include(u=>u.Orders)
            .FirstOrDefaultAsync();
        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);


        return userEntity;    }
}