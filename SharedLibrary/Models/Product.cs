using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SharedLibrary.Models;

public class Product:Base
{
    [JsonIgnore]
    public Company Company { get; set; } = new Company();
    
    [Column(TypeName = "varchar(50)")]
    public string CompanyId { get; set; } = string.Empty;
    public float Price { get; set; } = 0f;
    [Column(TypeName = "varchar(125)")]

    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; } = 0;
    
    [JsonIgnore] 
    public ProductDetail ProductDetail { get; set; }
    
    [Column(TypeName = "varchar(450)")]
    public string ImagePath { get; set; } = string.Empty;
    public float DiscountRate { get; set; } = 0f;
    
    [Column(TypeName = "varchar(250)")]
    public string Category { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Order> Orders { get; set; } = new List<Order>();

}