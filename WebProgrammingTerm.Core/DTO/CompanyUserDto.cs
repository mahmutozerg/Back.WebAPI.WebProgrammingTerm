using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;

public class CompanyUserDto
{
    [Required(ErrorMessage = "CompanyId is required")]
    public string CompanyId { get; set; } = string.Empty;

    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; } = string.Empty;
}
