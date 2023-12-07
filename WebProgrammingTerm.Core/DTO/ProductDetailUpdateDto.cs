namespace WebProgrammingTerm.Core.DTO;

public class ProductDetailUpdateDto
{
    public string ProductDetailId { get; set; }
     public string Author { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; } = DateTime.Now;
    public string Publisher { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Page { get; set; } = 0;
}