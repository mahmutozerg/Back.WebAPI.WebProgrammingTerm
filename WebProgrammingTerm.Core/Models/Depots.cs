using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgrammingTerm.Core.Models;

public class Depot:Base
{    
    [Column(TypeName = "varchar(50)")]
    public string City { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(50)")]
    public string Street { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(3)")]
    public string Country { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(20)")]

    public string Contact { get; set; } = string.Empty;
}