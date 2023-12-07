using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;
public class ProductUpdateDto
{
    [Required(ErrorMessage = "TargetProductId is required")]
    public string TargetProductId { get; set; } = string.Empty;

    [Range(0, float.MaxValue, ErrorMessage = "Price must be a non-negative number")]
    public float Price { get; set; } = 0f;

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative number")]
    public int Stock { get; set; } = 0;

    [Range(0, float.MaxValue, ErrorMessage = "DiscountRate must be a non-negative number")]
    public float DiscountRate { get; set; } = 0f;

    public string ImagePath { get; set; } = string.Empty;
}
