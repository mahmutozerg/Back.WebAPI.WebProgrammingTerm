using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;


namespace WebProgrammingTerm.Core.Services;

public interface IUserFavoriteService:IGenericService<UserFavorites>
{
    Task<CustomResponseDto<UserFavorites>> UpdateAsync(UserFavoritesDto userFavoritesDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<UserFavorites>> AddAsync(UserFavoritesDto userFavoritesDto, ClaimsIdentity claimsIdentity);
}