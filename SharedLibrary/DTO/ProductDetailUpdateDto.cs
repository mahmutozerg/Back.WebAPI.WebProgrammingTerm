using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;
public class ProductDetailUpdateDto
{
    [Required(ErrorMessage = "ProductDetailId is required")]
    public string ProductDetailId { get; set; } = string.Empty;

     public string Author { get; set; } = string.Empty;
     public string PublishDate { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
    
    public string Page { get; set; } = string.Empty;
}
