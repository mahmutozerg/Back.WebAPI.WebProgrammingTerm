using System;
using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO
{
    public class ProductDetailAddDto
    {
        [Required(ErrorMessage = "ProductId is required")]
        public string ProductId { get; set; } = string.Empty;

        [Required(ErrorMessage = "DepotId is required")]
        public string DepotId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "PublishDate is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid PublishDate format")]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Publisher is required")]
        public string Publisher { get; set; } = string.Empty;

        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = "Size is required")]
        public string Size { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Page must be a non-negative number")]
        public int Page { get; set; } = 0;
    }
}