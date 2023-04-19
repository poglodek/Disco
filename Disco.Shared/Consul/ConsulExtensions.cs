using System.Security.Authentication;
using Disco.Shared.Consul.HostedSerivces;
using Disco.Shared.Consul.HttpClient;
using Disco.Shared.Consul.Options;
using Disco.Shared.Consul.Services;
using Disco.Shared.Fabio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Shared.Consul;

public static class ConsulExtensions
{
    public static IServiceCollection AddConsul(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var options = configuration.GetSection("consul").Get<ConsulOptions>();
        serviceCollection.AddSingleton(_ => options);
        
        serviceCollection.AddSingleton<IConsulRegistration, ConsulRegistration>();
        serviceCollection.AddSingleton<IConsulService, ConsulService>();
        
        serviceCollection.AddHttpClient<ConsulHttpClient>(x =>
            {
                x.Timeout = TimeSpan.FromSeconds(30);
                x.BaseAddress = new Uri(options!.Url);
            
            });
        
        serviceCollection.AddSingleton<RegisterService>();
        serviceCollection.AddHostedService<RegisterService>();

        serviceCollection.AddFabio(configuration);
        
        return serviceCollection;
    }

    public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
    {
        app.Map("/ping", x =>
        {
            x.Run(async ctx =>
            {
                ctx.Response.StatusCode = 200;
                await ctx.Response.WriteAsync("pong");
            });
        });
        return app;
    }
}