using System.Text.Json.Serialization;

namespace WebProgrammingTerm.Core.Models;

public class UserComments:Base
{
    [JsonIgnore]
    public User User { get; set; } = new User();
    public string UserId { get; set; } = string.Empty;
    
    [JsonIgnore]
    public Product Product { get; set; } = new Product();
    public string ProductId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public float Rate { get; set; } = 0f;

}