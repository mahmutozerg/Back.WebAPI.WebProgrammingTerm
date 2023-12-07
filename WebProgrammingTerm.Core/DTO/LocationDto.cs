using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;

public class LocationDto
{
    [Required(ErrorMessage = "Country field is required")]
    public string Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "Street field is required")]
    public string Street { get; set; } = string.Empty;

    [Required(ErrorMessage = "PostalCode field is required")]
     public int PostalCode { get; set; } = 0;

    [Required(ErrorMessage = "No field is required")]
    public int No { get; set; } = 0;

    [Required(ErrorMessage = "PhoneNumber field is required")]
     public string PhoneNumber { get; set; } = string.Empty;
}
