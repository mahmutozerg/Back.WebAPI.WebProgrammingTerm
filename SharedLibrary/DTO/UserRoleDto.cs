using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class UserRoleDto
{
    [EmailAddress(ErrorMessage = "Wrong email format")]
    [Required(ErrorMessage = "UserMail field is required")]
    public string UserMail { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "RoleName field is required")]
    public string RoleName { get; set; } = string.Empty;
}