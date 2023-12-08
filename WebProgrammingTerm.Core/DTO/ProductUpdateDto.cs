using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;
public class ProductUpdateDto
{
    [Required(ErrorMessage = "TargetProductId is required")]
    public string TargetProductId { get; set; } = string.Empty;
    public float Price { get; set; } = 0f;
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; } = 0;
    public float DiscountRate { get; set; } = 0f;
    public string ImagePath { get; set; } = string.Empty;
}
