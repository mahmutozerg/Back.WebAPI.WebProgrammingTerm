using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;

public class CompanyDto
{
    public string Name { get; set; } = string.Empty;

    public string Contact { get; set; } = string.Empty; 
}