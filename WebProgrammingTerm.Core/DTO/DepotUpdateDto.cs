using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;
 
public class DepotUpdateDto
{
    [Required(ErrorMessage = "TargetDepotId is required")]
    public string TargetDepotId { get; set; } = string.Empty;

     public string City { get; set; } = string.Empty;

     public string Street { get; set; } = string.Empty;

     [StringLength(3, ErrorMessage = "Country must be a maximum of 3 characters")]
    public string Country { get; set; } = string.Empty;

     [StringLength(11, ErrorMessage = "Contact must be a maximum of 11 characters")]
    public string Contact { get; set; } = string.Empty;
}
