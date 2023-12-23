using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharedLibrary.DTO;

namespace WebProgrammingTerm.MVC.Models;

public class CartModel
{
    public List<ProductWCommentDto> Products { get; set; } = new List<ProductWCommentDto>();
    public List<Location> Locations { get; set; } = new List<Location>();
    
    [Required(ErrorMessage = "Please select an Address.")]
    public string ClickedLocation { get; set; } = string.Empty;

    [Required(ErrorMessage = "First Name is required.")]
    public string FirstNameCC { get; set; } = string.Empty;
    [Required(ErrorMessage = "Last Name is required.")]
    public string LastNameCC { get; set; } = string.Empty;
    
    
    [Required(ErrorMessage = "Credit Card number is required.")]
    public string CCNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "CVC is required.")]

    public int CVC { get; set; } 

}