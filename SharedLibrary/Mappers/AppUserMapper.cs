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

    public static User UpdateUser(User userEntity, AppUserUpdateDto updateDto)
    {
        userEntity.Email =  string.IsNullOrWhiteSpace(updateDto.Email) ? userEntity.Email : updateDto.Email;
        userEntity.Name =  string.IsNullOrWhiteSpace(updateDto.Name) ? userEntity.Name : updateDto.Name;
        userEntity.LastName =  string.IsNullOrWhiteSpace(updateDto.LastName) ? userEntity.LastName : updateDto.LastName;
        userEntity.BirthDate =  string.IsNullOrWhiteSpace(updateDto.BirthDate) ? userEntity.BirthDate : updateDto.BirthDate;
        userEntity.Gender = updateDto.Gender ?? userEntity.Gender;
        userEntity.IsDeleted = updateDto.IsDeleted;

        return userEntity;
    }

}