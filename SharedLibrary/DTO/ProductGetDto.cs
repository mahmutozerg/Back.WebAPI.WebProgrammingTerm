using SharedLibrary.Models;

namespace SharedLibrary.DTO;

public class ProductGetDto:ProductAddDto
{
    public Company Company { get; set; }
}