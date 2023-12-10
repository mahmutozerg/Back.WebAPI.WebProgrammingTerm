using System;
using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;
public class ProductDetailUpdateDto
{
    [Required(ErrorMessage = "ProductDetailId is required")]
    public string ProductDetailId { get; set; } = string.Empty;

     public string Author { get; set; } = string.Empty;
     public DateTime PublishDate { get; set; } = DateTime.Now;

    public string Publisher { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Page must be a non-negative number")]
    public int Page { get; set; } = 0;
}
