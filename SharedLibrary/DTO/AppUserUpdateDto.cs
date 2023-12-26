using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO;

public class AppUserUpdateDto
{
    public string? Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string BirthDate { get; set; } = string.Empty;
    public bool? Gender { get; set; }

    public bool IsDeleted { get; set; } = false;
}