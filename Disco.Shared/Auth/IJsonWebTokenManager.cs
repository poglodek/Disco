using Microsoft.IdentityModel.JsonWebTokens;

namespace Disco.Shared.Auth;

public interface IJsonWebTokenManager
{
    JWTokenDto CreateToken(Guid userId, string email, IDictionary<string, string> claims = null,
        Roles role = Roles.All);
}