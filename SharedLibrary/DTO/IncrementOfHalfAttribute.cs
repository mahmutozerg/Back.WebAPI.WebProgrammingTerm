using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

 public class IncrementOfHalfAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            // You can modify the error message to suit your needs
            return new ValidationResult("Rate is required.");
        }

        if (float.TryParse(value.ToString(), out var floatValue) && Math.Abs(floatValue % 0.5) < float.Epsilon)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}