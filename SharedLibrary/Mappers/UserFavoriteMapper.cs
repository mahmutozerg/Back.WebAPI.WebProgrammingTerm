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
    
    public static UserFavoritesListDto ToUserFavoritesListDto(List<UserFavorites?> userFavorites)
    {

        var userFavorite = new UserFavoritesListDto();

        foreach (var favorites in userFavorites)
        {
            userFavorite.UserFavoritesDtos.Add(favorites.ProductId);
        }


        return userFavorite;
    }
}