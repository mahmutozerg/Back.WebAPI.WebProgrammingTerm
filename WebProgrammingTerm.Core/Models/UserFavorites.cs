namespace WebProgrammingTerm.Core.Models;

public class UserFavorites:Base
{
    public User User { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;

    public Product Product { get; set; } 
    public string ProductId { get; set; }
}