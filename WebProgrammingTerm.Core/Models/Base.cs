namespace WebProgrammingTerm.Core.Models;

public class Base
{
    public string Id { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } =DateTime.Now;
    public DateTime UpdatedAt { get; set; } =DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}