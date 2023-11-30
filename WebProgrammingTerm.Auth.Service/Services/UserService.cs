using Microsoft.AspNetCore.Identity;
using WebProgrammingTerm.Auth.Core.DTOs;
using WebProgrammingTerm.Auth.Core.Models;
using WebProgrammingTerm.Auth.Core.Repositories;
using WebProgrammingTerm.Auth.Core.Services;

namespace WebProgrammingTerm.Auth.Service.Services;

public class UserService:GenericService<User>,IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IGenericRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(UserManager<User> userManager, IGenericRepository<User> repository, IUnitOfWork unitOfWork) :base(repository, unitOfWork)
    {
        _userManager = userManager;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
       
        var user = new User { Email = createUserDto.Email, UserName = createUserDto.UserName,CreatedAt = DateTime.Now,CreatedBy = "System"};
        var result = await _userManager.CreateAsync(user, createUserDto.Password);
        
        if (!result.Succeeded)
            return Response<UserAppDto>.Fail(result.Errors.Select(x => x.Description).ToList(), 400);
        
        return Response<UserAppDto>.Success(200);
    }

    public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return Response<UserAppDto>.Fail("Username does not exist", 404,true);
        
        return Response<UserAppDto>.Success(new UserAppDto()
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            
        }, 200);
    }

    public async Task<Response<NoDataDto>> Remove(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return Response<NoDataDto>.Fail("User id does not exist",404,true);
        
        _repository.Remove(user);
        await _unitOfWork.CommitAsync();

        return Response<NoDataDto>.Success(200);
    }
}