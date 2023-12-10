using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebProgrammingTerm.Core.Models;

public class Base
{
    [Required]
    public string Id { get; set; } = string.Empty;
    [Required]

    public string CreatedBy { get; set; } = string.Empty;
    [Required]

    public string UpdatedBy { get; set; } = string.Empty;
    [Required]

    public DateTime CreatedAt { get; set; } =DateTime.Now;
    [Required]

    public DateTime UpdatedAt { get; set; } =DateTime.Now;
    
    [JsonIgnore]
    [Required]
    public bool IsDeleted { get; set; } = false;
}