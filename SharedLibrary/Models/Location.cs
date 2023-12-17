using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SharedLibrary.Models;


public class Location:Base
{
    [JsonIgnore]

    public User User { get; set; } = new User();
    
    public string UserId { get; set; } = string.Empty;

    [Column(TypeName = "varchar(50)")]
    public int ZipCode { get; set; } = 0;

    [Column(TypeName = "varchar(250)")] 
    public string Address { get; set; } = string.Empty;
     
    [Column(TypeName = "varchar(50)")] 
    public string Title { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")] public string City { get; set; } = string.Empty;
}