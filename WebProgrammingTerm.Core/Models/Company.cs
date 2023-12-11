using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgrammingTerm.Core.Models;

public class Company:Base
{
    [Column(TypeName = "varchar(50)")]
    public string Id { get; set; } = string.Empty;
    [Column(TypeName = "varchar(120)")]
    public string Name { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string Contact { get; set; } = string.Empty; 

}