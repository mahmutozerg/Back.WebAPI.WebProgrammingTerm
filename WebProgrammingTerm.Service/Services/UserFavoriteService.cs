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

public class UserFavoriteService:GenericService<UserFavorites>,IUserFavoriteService
{
    private readonly IUserFavoritesRepository _userFavoritesRepository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;
    public UserFavoriteService( IUnitOfWork unitOfWork, IUserFavoritesRepository userFavoritesRepository, IUserService userService, IProductService productService) : base(userFavoritesRepository, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userFavoritesRepository = userFavoritesRepository;
        _userService = userService;
        _productService = productService;
    }

    public async Task<CustomResponseDto<UserFavorites>> UpdateAsync(UserFavoritesDto userFavoritesDto, ClaimsIdentity claimsIdentity)
    {
        var updatedBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var productExist = await _productService.Where(p => p != null && p.Id == userFavoritesDto.ProductId && !p.IsDeleted)
            .AsNoTracking().AnyAsync();
        
        if (!productExist)
            throw new Exception(ResponseMessages.ProductNotFound);

        var userEntity = await _userService.GetUserWithFavorites(updatedBy);

        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);

        var favoriteEntity = await _userFavoritesRepository.Where(uf => uf != null && uf.UserId == updatedBy && uf.ProductId == userFavoritesDto.ProductId ).SingleOrDefaultAsync();
        if (favoriteEntity == null) 
            throw new Exception(ResponseMessages.Notfound);
        
        favoriteEntity.IsDeleted = !favoriteEntity.IsDeleted;

        _userFavoritesRepository.Update(favoriteEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<UserFavorites>.Success(favoriteEntity, ResponseCodes.Updated);

    }

    public async Task<CustomResponseDto<UserFavorites>> AddAsync(UserFavoritesDto userFavoritesDto, ClaimsIdentity claimsIdentity)
    {
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var userEntity = await _userService.GetUserWithFavorites(createdBy);
        
        var productExist = await _productService.Where(p => p != null && p.Id == userFavoritesDto.ProductId && !p.IsDeleted)
            .AsNoTracking().AnyAsync();
        
        if (!productExist)
            throw new Exception(ResponseMessages.ProductNotFound);
        
        var favoritesEntity = await _userFavoritesRepository.Where(uf => uf != null && uf.UserId == createdBy && uf.ProductId == userFavoritesDto.ProductId).SingleOrDefaultAsync();
        
        if (favoritesEntity is not null)
            return await UpdateAsync(userFavoritesDto, claimsIdentity);

        favoritesEntity = UserFavoriteMapper.ToUserFavorites(userFavoritesDto);
        favoritesEntity.CreatedAt = DateTime.Now;
        favoritesEntity.CreatedBy = createdBy;
        favoritesEntity.UpdatedAt = DateTime.Now;
        favoritesEntity.UpdatedBy = createdBy;
        
        userEntity.Favorites.Add(favoritesEntity);

        await _userService.UpdateAsync(userEntity,createdBy);

        return CustomResponseDto<UserFavorites>.Success(favoritesEntity, ResponseCodes.Created);
    }

    public async Task<CustomResponseDto<UserFavoritesListDto>> GetAsync(ClaimsIdentity claimsIdentity)
    {
        var userFavoritesEntity = await _userService.GetFavorites(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        if (userFavoritesEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);

        var dto = UserFavoriteMapper.ToUserFavoritesListDto(userFavoritesEntity);


        return CustomResponseDto<UserFavoritesListDto>.Success(dto, 200);

    }
}