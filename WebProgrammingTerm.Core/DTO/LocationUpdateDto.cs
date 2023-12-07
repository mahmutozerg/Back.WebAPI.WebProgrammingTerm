using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;

public class LocationUpdateDto:LocationDto
{
    [Required(ErrorMessage = "Id field is required")]
    public string Id { get; set; } = string.Empty;
}