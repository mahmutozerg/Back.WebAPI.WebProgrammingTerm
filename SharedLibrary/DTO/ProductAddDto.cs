﻿using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class ProductAddDto
{
 

    [Required(ErrorMessage = "Price is required")]
    public float Price { get; set; } = 0f;

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Stock is required")]
    public int Stock { get; set; } = 0;

    [Required(ErrorMessage = "DiscountRate is required")]
    public float DiscountRate { get; set; } = 0f;

    [Required(ErrorMessage = "ImagePath is required")]
    public string ImagePath { get; set; } = string.Empty;
    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; } = string.Empty;

  
    [Required(ErrorMessage = "DepotId is required")]
    public string DepotId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "PublishDate is required")]
     public string PublishDate { get; set; } = String.Empty;

    [Required(ErrorMessage = "Publisher is required")]
    public string Publisher { get; set; } = string.Empty;

    [Required(ErrorMessage = "Language is required")]
    public string Language { get; set; } = string.Empty;

    [Required(ErrorMessage = "Size is required")]
    public string Size { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product is required")]

    public string Page { get; set; } = string.Empty;
}