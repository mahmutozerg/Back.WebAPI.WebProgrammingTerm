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

    public async Task<CustomResponseDto<CustomResponseNoDataDto>> AddUserByIdAsync(string id)
    {
        var userExist = await _userRepository.AnyAsync(u => u != null && u.Id == id);
        if (userExist)
            return CustomResponseDto<CustomResponseNoDataDto>.Fail("User already exist",409);

        var user = new User()
        {
            Name = string.Empty,
            LastName = string.Empty,
            Id = id,
            IsDeleted = false,
        };
        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<CustomResponseNoDataDto>.Success(201);

    }
}