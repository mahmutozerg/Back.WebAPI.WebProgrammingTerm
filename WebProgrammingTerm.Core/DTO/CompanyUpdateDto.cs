using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO
{
    public class CompanyUpdateDto
    {
        [Required(ErrorMessage = "TargetId is required")]
        public string TargetId { get; set; } = string.Empty;

         public string Name { get; set; } = string.Empty;

         public string Contact { get; set; } = string.Empty; 
    }
}