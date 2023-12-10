namespace WebProgrammingTerm.Core.DTO;
using System.ComponentModel.DataAnnotations;

public class ProductAddDto
{
 

    [Required(ErrorMessage = "Price is required")]
    public float Price { get; set; } = 0f;

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Stock is required")]
    public int Stock { get; set; } = 0;

    [Required(ErrorMessage = "DiscountRate is required")]
    public float DiscountRate { get; set; } = 0f;

    [Required(ErrorMessage = "ImagePath is required")]
    public string ImagePath { get; set; } = string.Empty;
}