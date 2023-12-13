using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SharedLibrary.Models;

public class ProductDetail:Base
{
    [JsonIgnore]
    public Product Product { get; set; }
    [Column(TypeName = "varchar(50)")]

    public string ProductId { get; set; } = string.Empty;
    [JsonIgnore]
    public Depot Depot { get; set; } = new Depot();
    [Column(TypeName = "varchar(50)")]
    public string DepotId { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(120)")]
    public string PublishDate { get; set; } = string.Empty;
    [Column(TypeName = "varchar(50)")]

    public string Publisher { get; set; } = string.Empty;
    [Column(TypeName = "varchar(25)")]
    public string Language { get; set; } = string.Empty;
    [Column(TypeName = "varchar(50)")]

    public string Size { get; set; } = string.Empty;
    public string Page { get; set; } = string.Empty;
}