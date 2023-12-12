using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

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