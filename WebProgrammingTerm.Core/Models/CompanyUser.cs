using System.Text.Json.Serialization;

namespace WebProgrammingTerm.Core.Models;

public class CompanyUser:Base
{
    [JsonIgnore]
    public Company Company { get; set; } = new Company();
    public string CompanyId { get; set; } = string.Empty;
    [JsonIgnore]
    public User User { get; set; } = new User();
    public string UserId { get; set; } = string.Empty;

    public string UserMail { get; set; } = string.Empty;

}