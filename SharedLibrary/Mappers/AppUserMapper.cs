using Newtonsoft.Json.Linq;
using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

public static class AppUserMapper
{
    public static User ToUser(UserAddDto userAddDto)
    {
        return new User()
        {
            Name = string.IsNullOrEmpty(userAddDto.Name) ? string.Empty: userAddDto.Name,
            LastName = string.IsNullOrEmpty(userAddDto.LastName) ? string.Empty: userAddDto.LastName,
            Id = userAddDto.Id,
            IsDeleted = false,
            Email = userAddDto.Email,
        };
    }


}