using Disco.Service.Users.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Users.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services
            .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
}