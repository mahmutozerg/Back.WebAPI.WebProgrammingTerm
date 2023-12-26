using SharedLibrary.Models;

namespace SharedLibrary.DTO;

public class ProductGetDto:ProductAddDto
{
    public Company Company { get; set; }
    public string ProductId { get; set; }

    public ProductDetail ProductDetail { get; set; }

    public bool IsDeleted { get; set; } = false;
}