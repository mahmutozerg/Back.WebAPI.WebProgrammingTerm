﻿using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SharedLibrary.Models;

public class UserFavorites:Base
{
    [JsonIgnore]
    public AppUser AppUser { get; set; } = null!;
    [Column(TypeName = "varchar(50)")]

    public string UserId { get; set; } = string.Empty;
    [JsonIgnore]

    public Product Product { get; set; } 
    [Column(TypeName = "varchar(50)")]

    public string ProductId { get; set; }
}