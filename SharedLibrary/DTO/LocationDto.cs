using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class LocationDto
{

    [Required(ErrorMessage = "Zip code field is required")]
     public int ZipCode { get; set; } = 0;

     [Required(ErrorMessage = "Address field is required")]
     public string Address { get; set; } = string.Empty;
     
     [Required(ErrorMessage = "Title field is required")]
     public string Title { get; set; } = string.Empty;

     
     [Required(ErrorMessage = "City field is required")]

     public string City { get; set; }
}
