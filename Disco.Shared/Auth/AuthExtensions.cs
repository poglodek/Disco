using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Disco.Shared.Auth;

public static class AuthExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var options = configuration.GetSection("auth").Get<AuthOptions>();
        
        var key = new X509SecurityKey(new X509Certificate2(options.Url!,options.Password!));
        
        serviceCollection.AddSingleton(_ => options);
        serviceCollection.AddAuthorization(x =>
        {
            x.AddPolicy("certificate", p =>
            {
                p.AddAuthenticationSchemes(CertificateAuthenticationDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser();
            });
        });
        
        serviceCollection.AddAuthentication(authenticationOptions =>
        {
            authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.RequireHttpsMetadata = false;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.JwtIssuer,
                    ValidAudience  = options.JwtIssuer,
                    IssuerSigningKey = key
                };
            });

        serviceCollection.AddSingleton<IJsonWebTokenManager, JsonWebTokenManager>();
        serviceCollection.AddSingleton<SecurityKeyCert>( _ => new (key));
        
        return serviceCollection;
    }

    public record SecurityKeyCert(SecurityKey key);
}