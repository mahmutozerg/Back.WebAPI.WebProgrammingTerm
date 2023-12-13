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

public class UserCommentService:GenericService<UserComments>,IUserCommentService
{
    private readonly IUserCommentRepository _userCommentRepository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;
    public UserCommentService( IUnitOfWork unitOfWork, IUserCommentRepository userCommentRepository, IUserService userService, IProductService productService) : base(userCommentRepository, unitOfWork)
    {
        _userCommentRepository = userCommentRepository;
        _userService = userService;
        _productService = productService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponseDto<UserComments>> UpdateAsync(UserCommentUpdateDto userCommentUpdateDto, ClaimsIdentity claimsIdentity)
    {
        var updatedBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userEntity = await _userService.GetUserWithComments(updatedBy);
        var productEntity = await _productService.GetProductWithCompany(userCommentUpdateDto.ProductId);
        
        var userCommentEntity = await _userCommentRepository.Where(uc =>
            uc != null && uc.ProductId == userCommentUpdateDto.ProductId && uc.UserId == updatedBy && !uc.IsDeleted).FirstOrDefaultAsync();

        if (userCommentEntity is null) 
            throw new Exception(ResponseMessages.UserCommentExist);

        userCommentEntity.Rate = userCommentUpdateDto.Rate == 0 ? userCommentEntity.Rate : userCommentUpdateDto.Rate;
        userCommentEntity.Title = string.IsNullOrEmpty(userCommentUpdateDto.Title) ? userCommentEntity.Title : userCommentUpdateDto.Title;
        userCommentEntity.Content = string.IsNullOrEmpty(userCommentUpdateDto.Content)? userCommentEntity.Content: userCommentUpdateDto.Content;
        userCommentEntity.UpdatedBy = updatedBy;
        _userCommentRepository.Update(userCommentEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<UserComments>.Success(userCommentEntity, ResponseCodes.Updated);

    }

    public async Task<CustomResponseDto<UserComments>> AddAsync(UserCommentAddDto userCommentAddDto, ClaimsIdentity claimsIdentity)
    {
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userEntity = await _userService.GetUserWithComments(createdBy);
        
        if (string.IsNullOrEmpty(userEntity.Name) || string.IsNullOrEmpty(userEntity.LastName))
            throw new Exception(ResponseMessages.UserCommentCredentialNotFound);

        var productEntity = await _productService.GetProductWithCompany(userCommentAddDto.ProductId);
        
        var commentExist = await _userCommentRepository.Where(uc =>
            uc != null && uc.ProductId == userCommentAddDto.ProductId && uc.UserId == createdBy && !uc.IsDeleted).AnyAsync();

        if (commentExist)
            throw new Exception(ResponseMessages.UserCommentExist);

        var  userCommentEntity = UserCommentMapper.ToUserComment(userCommentAddDto);
        userCommentEntity.AppUser = userEntity;
        userCommentEntity.Product = productEntity;
        userCommentEntity.CreatedBy = createdBy;
        userCommentEntity.UpdatedBy = createdBy;
        userCommentEntity.CreatedAt = DateTime.Now;
        userCommentEntity.UpdatedAt = DateTime.Now;

        await _userCommentRepository.AddAsync(userCommentEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<UserComments>.Success(userCommentEntity, ResponseCodes.Created);
     }
    
}