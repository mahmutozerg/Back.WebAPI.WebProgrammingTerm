using System.Text.Json.Serialization;

namespace WebProgrammingTerm.Core.Models;

public class Order : Base
{
    [JsonIgnore]
    public User User { get; set; } = new User();
    public string UserId { get; set; } = string.Empty;
    public string Shipment { get; set; } = string.Empty;
    public Location Location { get; set; } = new Location();
    public string LocationId { get; set; } = string.Empty; 
    public OrderDetail OrderDetail { get; set; } = new OrderDetail();
    public List<Product> Products { get; set; }

 }