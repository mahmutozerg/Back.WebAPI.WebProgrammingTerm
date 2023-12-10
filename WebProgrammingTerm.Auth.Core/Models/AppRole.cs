using Microsoft.AspNetCore.Identity;

namespace WebProgrammingTerm.Auth.Core.Models;

public class AppRole:IdentityRole
{
    public AppRole(string name):base(name)
    {
        
    }
}