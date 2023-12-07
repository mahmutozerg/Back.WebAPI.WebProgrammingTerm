using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
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

    public async Task<CustomResponseDto<UserFavorites>> UpdateAsync(UserFavoritesDto userFavoritesDto, string updatedBy)
    {
        var productExist = await _productService.Where(p => p.Id == userFavoritesDto.ProductId && !p.IsDeleted)
            .AsNoTracking().AnyAsync();
        
        if (!productExist)
            throw new Exception(ResponseMessages.ProductNotFound);
        
        var userEntity = await _userService
            .Where(u => u.Id == updatedBy && !u.IsDeleted)
            .Include(u=>u.Favorites)
            .SingleOrDefaultAsync();

        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);

        var favoriteEntity = await _userFavoritesRepository.Where(uf => uf.UserId == updatedBy).SingleOrDefaultAsync();
        favoriteEntity.IsDeleted = !favoriteEntity.IsDeleted;
        
        _userFavoritesRepository.Update(favoriteEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<UserFavorites>.Success(favoriteEntity, ResponseCodes.Updated);
    }

    public async Task<CustomResponseDto<UserFavorites>> AddAsync(UserFavoritesDto userFavoritesDto, string createdBy)
    {
        var productExist = await _productService.Where(p => p.Id == userFavoritesDto.ProductId && !p.IsDeleted)
            .AsNoTracking().AnyAsync();
        
        if (!productExist)
            throw new Exception(ResponseMessages.ProductNotFound);
        
        var favoritesEntity = await _userFavoritesRepository.Where(uf => uf.UserId == createdBy).SingleOrDefaultAsync();
        
        if (favoritesEntity is not null)
            return await UpdateAsync(userFavoritesDto, createdBy);

        var userEntity = await _userService
            .Where(u => u.Id == createdBy && !u.IsDeleted)
            .Include(u=>u.Favorites)
            .SingleOrDefaultAsync();

        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);
        
        
        var favoriteEntity = UserFavoriteMapper.ToUserFavorites(userFavoritesDto);
         favoriteEntity.CreatedAt = DateTime.Now;
         favoriteEntity.CreatedBy = createdBy;
         favoriteEntity.UpdatedAt = DateTime.Now;
         favoriteEntity.UpdatedBy = createdBy;
        
        userEntity.Favorites.Add(favoriteEntity);

        await _userService.UpdateAsync(userEntity,createdBy);

        return CustomResponseDto<UserFavorites>.Success(favoriteEntity, ResponseCodes.Created);
    }
}