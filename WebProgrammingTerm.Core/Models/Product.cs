﻿using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace WebProgrammingTerm.Core.Models;

public class Product:Base
{
     [JsonIgnore]
    public Company Company { get; set; } = new Company();
    public string CompanyId { get; set; } = string.Empty;
    public float Price { get; set; } = 0f;
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; } = 0;
    public ProductDetail ProductDetail { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public float DiscountRate { get; set; } = 0f;
    [JsonIgnore]

    public List<Order> Orders { get; set; } = new List<Order>();
}