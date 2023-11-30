using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Auth.Core.DTOs;

public class CreateUserDto
{
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [Required(ErrorMessage = "Email field is required")]
    public string Email { get; set; } = "empty@testapp.com";

    [Required(ErrorMessage = "UserName field is required")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password field is required")]
    public string Password { get; set; } = string.Empty;
}