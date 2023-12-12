using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class LocationUpdateDto:LocationDto
{
    [Required(ErrorMessage = "Id field is required")]
    public string Id { get; set; } = string.Empty;
}