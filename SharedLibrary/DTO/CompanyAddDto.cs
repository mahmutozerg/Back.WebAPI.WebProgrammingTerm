using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;
 
    public class CompanyAddDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact is required")]
        public string Contact { get; set; } = string.Empty; 
    }
 