using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SharedLibrary.Models;

public class Order : Base
{
    [JsonIgnore]
    public AppUser AppUser { get; set; } = new AppUser();
    
    [Column(TypeName = "varchar(50)")]
    public string UserId { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(25)")]
    public string Shipment { get; set; } = string.Empty;
    public Location Location { get; set; } = new Location();
    
    [Column(TypeName = "varchar(50)")]
    public string LocationId { get; set; } = string.Empty; 
    public OrderDetail OrderDetail { get; set; }
    public List<Product> Products { get; set; }

}