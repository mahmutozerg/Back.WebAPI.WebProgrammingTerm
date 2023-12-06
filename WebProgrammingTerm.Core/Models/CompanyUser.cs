namespace WebProgrammingTerm.Core.Models;

public class CompanyUser
{
    public Company Company { get; set; } = new Company();
    public string CompanyId { get; set; } = string.Empty;
    public User User { get; set; } = new User();
    public string UserId { get; set; } = string.Empty;

}