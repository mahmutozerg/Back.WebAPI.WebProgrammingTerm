﻿using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SharedLibrary.Models;

public class CompanyUser:Base
{
    [JsonIgnore]
    public Company Company { get; set; } = new Company();
    public string CompanyId { get; set; } = string.Empty;
    [JsonIgnore]
    public AppUser AppUser { get; set; } = new AppUser();
    public string UserId { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(120)")]

    public string Email { get; set; } = string.Empty;

}