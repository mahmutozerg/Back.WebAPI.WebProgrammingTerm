namespace WebProgrammingTerm.Core.Models;

public class Images:Base
{
    public Product Product { get; set; } = new Product();
    public string ProductId { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
}