using System.ComponentModel.DataAnnotations;
using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace WebProgrammingTerm.MVC.Models;

public class UserModels
{
    public AppUserUpdateDto AppUserUpdateDto { get; set; } = new AppUserUpdateDto();
    public UserUpdatePasswordDto UserUpdatePasswordDto { get; set; } = new UserUpdatePasswordDto();
    public User User { get; set; } = new User();
}