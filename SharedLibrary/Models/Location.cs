using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SharedLibrary.Models;

public class Location:Base
{
    [JsonIgnore]
    public User User { get; set; } = new User();
    
    public string UserId { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string Country { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string Street { get; set; } = string.Empty;
    
    public int PostalCode { get; set; } = 0;
    
    public int No { get; set; } = 0;
    
    
    [Column(TypeName = "varchar(20)")]
    public string PhoneNumber { get; set; } = string.Empty;

}