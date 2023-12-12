using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SharedLibrary.Models;

public class Base
{
    [Column(TypeName = "varchar(50)")]
    public string Id { get; set; } = string.Empty;
    
    
    [Column(TypeName = "varchar(50)")]
    public string CreatedBy { get; set; } = string.Empty;
    
    
    [Column(TypeName = "varchar(50)")]
    public string UpdatedBy { get; set; } = string.Empty;
    
    
    [Column(TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; } =DateTime.Now;
    
    [Column(TypeName = "datetime2")]
    public DateTime UpdatedAt { get; set; } =DateTime.Now;
    
    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
}