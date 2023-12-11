using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgrammingTerm.Auth.Core.Models;

public class Base
{
    [Column(TypeName = "datetime2")]

    public DateTime? CreatedAt { get; set; }
    public String? CreatedBy { get; set; }
    [Column(TypeName = "datetime2")]

    public DateTime? UpdatedAt { get; set; }
    public String? UpdatedBy { get; set; }
}