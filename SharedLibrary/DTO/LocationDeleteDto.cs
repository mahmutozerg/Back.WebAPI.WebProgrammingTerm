using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class LocationDeleteDto
{
    [Required(ErrorMessage = "LocationId field required")]
    public string LocationId { get; set; } = string.Empty;
}