using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebProgrammingTerm.Core.Models;

public class UserFavorites:Base
{
    [JsonIgnore]
    public User User { get; set; } = null!;
    [Column(TypeName = "varchar(50)")]

    public string UserId { get; set; } = string.Empty;
    [JsonIgnore]

    public Product Product { get; set; } 
    [Column(TypeName = "varchar(50)")]

    public string ProductId { get; set; }
}