namespace WebProgrammingTerm.Auth.Core.DTOs;

public class TokenDto
{
    public string AccesssToken { get; set; } = string.Empty;
    public DateTime AccesTokenExpiration { get; set; }


    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiration { get; set; }
}