using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.Models;

public class User:Base
{
    [Column(TypeName = "varchar(50)")]

    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "varchar(50)")]
    public string LastName { get; set; } = string.Empty;
    public List<UserComments> Comments { get; set; } = new List<UserComments>();
    public List<Location> Locations { get; set; } = new List<Location>();
    public List<UserFavorites> Favorites { get; set; } = new List<UserFavorites>();
    public List<Order> Orders { get; set; } = new List<Order>();
    [Column(TypeName = "varchar(120)")]
    public string Email { get; set; } = string.Empty;

    public int Age { get; set; } = 0;
    public string BirthDate { get; set; } = string.Empty;

}