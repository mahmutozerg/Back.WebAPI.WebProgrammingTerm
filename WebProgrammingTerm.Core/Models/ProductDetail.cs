using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace WebProgrammingTerm.Core.Models;

public class ProductDetail:Base
{
    public string Id { get; set; }
    [JsonIgnore]
    public Product Product { get; set; }
 
    public string ProductId { get; set; } = string.Empty;
    [JsonIgnore]
    public Depot Depot { get; set; } = new Depot();
    public string DepotId { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; } = DateTime.Now;
    public string Publisher { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public int Page { get; set; } = 0;
}