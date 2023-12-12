using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class DepotAddDto
{
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Street is required")]
    public string Street { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country is required")]
    [StringLength(3, ErrorMessage = "Country must be a maximum of 3 characters")]
    public string Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact is required")]
    [StringLength(21, ErrorMessage = "Contact must be a maximum of 10 characters")]

    public string Contact { get; set; } = string.Empty;
}
