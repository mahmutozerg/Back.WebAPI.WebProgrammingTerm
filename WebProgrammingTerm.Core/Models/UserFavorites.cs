using System.Text.Json.Serialization;

namespace WebProgrammingTerm.Core.Models;

public class UserFavorites:Base
{
    [JsonIgnore]
    public User User { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    [JsonIgnore]

    public Product Product { get; set; } 
    public string ProductId { get; set; }
}