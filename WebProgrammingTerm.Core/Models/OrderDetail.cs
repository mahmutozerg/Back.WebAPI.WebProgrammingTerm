using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace WebProgrammingTerm.Core.Models;

public class OrderDetail
{    
    
    [JsonIgnore]
    public Order Order { get; set; } = new Order();
    [Column(TypeName = "varchar(50)")]

    public string OrderId { get; set; } = string.Empty;

    public float Tax { get; set; } = 0f;
    
    [Column(TypeName = "varchar(25)")]
    public string PaymentMethod { get; set; } = string.Empty;
}