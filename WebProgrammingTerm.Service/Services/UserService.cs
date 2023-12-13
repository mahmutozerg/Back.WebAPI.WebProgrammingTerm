﻿using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;

namespace WebProgrammingTerm.Service.Services;

public class UserService:GenericService<AppUser>,IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository) : base(userRepository,unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<CustomResponseDto<AppUser>> AddUserAsync(UserAddDto userAddDto,ClaimsIdentity claimsIdentity)
    {
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userExist = await _userRepository.AnyAsync(u => u != null && u.Email == userAddDto.Email);
        if (userExist)
            throw new Exception(ResponseMessages.UserAlreadyExist);

        var user = new AppUser()
        {
            Name = string.IsNullOrEmpty(userAddDto.Name) ? string.Empty: userAddDto.Name,
            LastName = string.IsNullOrEmpty(userAddDto.LastName) ? string.Empty: userAddDto.LastName,
            Id = userAddDto.Id,
            IsDeleted = false,
            Email = userAddDto.Email,
            CreatedBy = createdBy,
            UpdatedBy = createdBy
            
        };
        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<AppUser>.Success(user,ResponseCodes.Created);

    }

    public async Task<AppUser> GetUserWithComments(string id)
    {
        var user= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u=>u!.Comments)
            .SingleOrDefaultAsync();

        if (user is null)
            new Exception(ResponseMessages.UserNotFound);

        return user;
    }

    public async Task<AppUser> GetUserWithFavorites(string id)
    {
        var user= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u=>u!.Favorites)
            .SingleOrDefaultAsync();

        if (user is null)
            new Exception(ResponseMessages.UserNotFound);

        return user;    }

    public async Task<AppUser> GetUserWithLocations(string id)
    {
       var userEntity= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u=>u.Locations)
            .FirstOrDefaultAsync();
        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);


        return userEntity;
    }

    public async Task<AppUser> GetUserWithOrders(string id)
    {
        var userEntity= await _userRepository
            .Where(u => u != null && u.Id == id && !u.IsDeleted)
            .Include(u=>u.Orders)
            .FirstOrDefaultAsync();
        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);


        return userEntity;    }
}