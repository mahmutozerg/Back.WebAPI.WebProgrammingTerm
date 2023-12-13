using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SharedLibrary.Models;

public class UserComments:Base
{
    [JsonIgnore]
    public AppUser AppUser { get; set; } = new AppUser();
    [Column(TypeName = "varchar(50)")]

    public string UserId { get; set; } = string.Empty;
    
    [JsonIgnore]
    public Product Product { get; set; } = new Product();
    [Column(TypeName = "varchar(50)")]

    public string ProductId { get; set; } = string.Empty;
    [Column(TypeName = "varchar(50)")]

    public string Title { get; set; } = string.Empty;
    [Column(TypeName = "varchar(250)")]

    public string Content { get; set; } = string.Empty;
    public float Rate { get; set; } = 0f;

}