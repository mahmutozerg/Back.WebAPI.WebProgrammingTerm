using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;

public class UserAddDto
{
    [Required(ErrorMessage = "Id field is required")]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "MailAddress field is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string MailAddress { get; set; } = string.Empty;
}