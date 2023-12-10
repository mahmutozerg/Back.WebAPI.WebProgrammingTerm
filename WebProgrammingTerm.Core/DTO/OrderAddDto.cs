using System.Text.Json.Serialization;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.DTO;

public class OrderAddDto
{
 
    public string Shipment { get; set; } = string.Empty;
    public string LocationId { get; set; } = string.Empty;
     public List<string> ProductIdList { get; set; } = new List<string>();
}