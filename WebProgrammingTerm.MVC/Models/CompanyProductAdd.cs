using System;
using System.Collections.Generic;
using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace WebProgrammingTerm.MVC.Models;

public class CompanyProductAdd
{
    public List<Depot> Depots { get; set; } = new List<Depot>();

    public ProductAddDto ProductAddDto { get; set; }
}