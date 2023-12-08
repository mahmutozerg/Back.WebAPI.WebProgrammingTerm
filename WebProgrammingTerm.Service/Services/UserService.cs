using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;
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

    public async Task<CustomResponseDto<User>> AddUserByIdAsync(string id,string createdBy)
    {
        var userExist = await _userRepository.AnyAsync(u => u != null && u.Id == id);
        if (userExist)
            throw new Exception(ResponseMessages.UserAlreadyExist);

        var user = new User()
        {
            Name = string.Empty,
            LastName = string.Empty,
            Id = id,
            IsDeleted = false,
            CreatedBy = createdBy,
            UpdatedBy = createdBy
        };
        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<User>.Success(user,ResponseCodes.Created);

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