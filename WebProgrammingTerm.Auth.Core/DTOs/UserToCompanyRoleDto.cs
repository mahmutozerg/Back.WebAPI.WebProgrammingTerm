using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Auth.Core.DTOs;

public class UserToCompanyRoleDto
{
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Required(ErrorMessage = "UserMail field is required")]
    public string UserMail { get; set; } = string.Empty;
}