namespace WebProgrammingTerm.Core.DTO;

public class ProductDetailAddDto
{
    public string ProductId { get; set; } = string.Empty;
    public string DepotId { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; } = DateTime.Now;
    public string Publisher { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Page { get; set; } = 0;
}