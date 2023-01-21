using Disco.Shared.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Points.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
            .AddAuth(configuration);





}