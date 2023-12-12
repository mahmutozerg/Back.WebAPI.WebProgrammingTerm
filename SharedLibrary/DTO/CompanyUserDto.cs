using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class CompanyUserDto
{
 
    [Required(ErrorMessage = "UserMail field is required")]
    [EmailAddress(ErrorMessage = "Invalid mail address")]
    public string UserMail { get; set; } = string.Empty;
}
