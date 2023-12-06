using System.Runtime.CompilerServices;

namespace WebProgrammingTerm.Core.Models;

public class Product:Base
{
    public Company Company { get; set; } = new Company();
    public string CompanyId { get; set; } = string.Empty;
    public float Price { get; set; } = 0f;
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; } = 0;
}