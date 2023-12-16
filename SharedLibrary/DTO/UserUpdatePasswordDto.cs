using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class UserUpdatePasswordDto
{
    [Required] public string OldPassword { get; set; } = string.Empty;

    [Required] public string NewPassword { get; set; } = string.Empty;

    [Required] public string NewPasswordConfirm { get; set; } = string.Empty;
}