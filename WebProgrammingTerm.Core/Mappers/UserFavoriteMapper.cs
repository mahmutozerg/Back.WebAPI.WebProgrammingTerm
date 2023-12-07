using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Mappers;

public static class UserFavoriteMapper
{
    public static UserFavorites ToUserFavorites(UserFavoritesDto userFavoritesDto)
    {
        var userFavorite = new UserFavorites()
        {
            Id = Guid.NewGuid().ToString(),
            ProductId = userFavoritesDto.ProductId
        };

        return userFavorite;
    }
}