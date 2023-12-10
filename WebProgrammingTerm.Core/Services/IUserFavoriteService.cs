using System.Security.Claims;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IUserFavoriteService:IGenericService<UserFavorites>
{
    Task<CustomResponseDto<UserFavorites>> UpdateAsync(UserFavoritesDto userFavoritesDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<UserFavorites>> AddAsync(UserFavoritesDto userFavoritesDto, ClaimsIdentity claimsIdentity);
}