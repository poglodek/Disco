namespace Disco.Shared.Auth;

public class JWTokenDto
{
    public string JWT { get; set; }
    public Guid UserId { get; set; }
    public int ExpiresInHours { get; set; }
}