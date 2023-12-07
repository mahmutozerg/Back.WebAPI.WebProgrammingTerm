using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;

public class UserFavoritesDto
{
    [Required(ErrorMessage = "ProductId field is required")]
    public string ProductId { get; set; } = string.Empty;
}