using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Auth.Core.DTO;

public class CUserToCompanyRoleDto
{
    [Required(ErrorMessage = "UserMail field is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string UserMail { get; set; } = string.Empty;
}