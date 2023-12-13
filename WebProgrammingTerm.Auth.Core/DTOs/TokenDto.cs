namespace WebProgrammingTerm.Auth.Core.DTOs;

public class TokenDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiration { get; set; }
    
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiration { get; set; }
}