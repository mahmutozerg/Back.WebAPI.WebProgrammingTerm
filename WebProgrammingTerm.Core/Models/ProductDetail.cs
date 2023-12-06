using System.Security.Cryptography.X509Certificates;

namespace WebProgrammingTerm.Core.Models;

public class ProductDetail
{
    public string Id { get; set; }
    public Product Product { get; set; } = new Product();
    public string ProductId { get; set; } = string.Empty;
    public Depot Depot { get; set; } = new Depot();
    public string DepotId { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; } = DateTime.Now;
    public string Publisher { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Page { get; set; } = 0;
    public float DiscountRate { get; set; } = 0f;
}