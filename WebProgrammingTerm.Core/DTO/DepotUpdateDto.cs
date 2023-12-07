namespace WebProgrammingTerm.Core.DTO;

public class DepotUpdateDto
{
    public string TargetDepotId { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
}