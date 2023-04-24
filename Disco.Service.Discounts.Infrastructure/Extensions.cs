using Disco.Service.Discounts.Infrastructure.Middleware;
using Disco.Shared.Consul;
using Disco.Shared.Rabbit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Discounts.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
          //  .AddMongo<PointsDocument,Guid>(configuration)
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            //.AddSingleton<IPointsRepository,PointsRepository>()
            .AddRabbitMQ(configuration)
            .AddScoped<CustomMiddleware>()
            .AddConsul(configuration);

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        => app
            .UseMiddleware<CustomMiddleware>()
            .UseConsul()
            .UseRabbitMQ();
}