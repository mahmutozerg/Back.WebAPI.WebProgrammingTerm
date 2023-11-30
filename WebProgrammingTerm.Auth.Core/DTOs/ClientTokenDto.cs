namespace WebProgrammingTerm.Auth.Core.DTOs;

public class ClientTokenDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiration { get; set; }
}