namespace WebProgrammingTerm.Core.Models;

public class Company:Base
{
    public string Name { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public List<User> Users { get; set; } = new List<User>();
}