namespace WebProgrammingTerm.Core.Models;

public class UserFavorites:Base
{
    public User User { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;

    public List<Product> Product { get; set; } = new List<Product>();
}