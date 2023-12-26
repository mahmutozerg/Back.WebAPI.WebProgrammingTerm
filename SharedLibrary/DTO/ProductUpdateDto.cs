using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;
public class ProductUpdateDto
{
    [Required(ErrorMessage = "TargetProductId is required")]
    public string TargetProductId { get; set; } = string.Empty;
    
    public float Price { get; set; } = 0f;

    public string Name { get; set; } = string.Empty;

    public int Stock { get; set; } = 0;

    public float DiscountRate { get; set; } = 0f;

    public string ImagePath { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    
    public string DepotId { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string PublishDate { get; set; } = String.Empty;

    public string Publisher { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;
    public string Page { get; set; } = string.Empty;

    public bool IsDeleted { get; set; } = false;
}
