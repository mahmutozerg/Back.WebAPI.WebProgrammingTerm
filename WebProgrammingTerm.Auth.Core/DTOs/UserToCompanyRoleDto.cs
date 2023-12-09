using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Auth.Core.DTOs;

public class UserToCompanyRoleDto
{    
    [Required(ErrorMessage = "UserMail field is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string UserMail { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "CompanyId field is required")]

    public string CompanyId { get; set; } = string.Empty;
}