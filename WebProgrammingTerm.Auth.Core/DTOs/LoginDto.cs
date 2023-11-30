using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Auth.Core.DTOs;

public class LoginDto
{
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [Required(ErrorMessage = "Email field is required")]
    public string Email { get; set; } = "empty@testapp.com";

    [Required(ErrorMessage = "Password field is required")]
    public string Password { get; set; } = string.Empty;
}