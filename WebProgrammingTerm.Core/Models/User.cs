namespace WebProgrammingTerm.Core.Models;

public class User:Base
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<UserComments> Comments { get; set; } = new List<UserComments>();
    public List<Location> Locations { get; set; } = new List<Location>();
    public List<UserFavorites> Favorites { get; set; } = new List<UserFavorites>();
    public List<Order> Orders { get; set; } = new List<Order>();

}