using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class UserCommentUpdateDto
{
    public string ProductId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
 
    
    [Range(1, 5.0, ErrorMessage = "Rate must be between 1.0 and 5.0.")]
    [IncrementOfHalf(ErrorMessage = "Rate must be in increments of 0.5.")]

    public float Rate { get; set; }
}