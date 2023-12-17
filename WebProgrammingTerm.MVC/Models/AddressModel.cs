using System.Collections.Generic;
using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace WebProgrammingTerm.MVC.Models;

public class AddressModel
{
    public List<Location> locations { get; set; }
    public LocationUpdateDto locationUpdateDtop { get; set; }
}