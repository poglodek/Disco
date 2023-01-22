using Disco.Service.Points.Core.Repositories;
using Disco.Service.Points.Infrastructure.Middleware;
using Disco.Service.Points.Infrastructure.Mongo.Documents;
using Disco.Service.Points.Infrastructure.Mongo.Repositories;
using Disco.Shared.Consul;
using Disco.Shared.Mongo;
using Disco.Shared.Rabbit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Points.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddMongo<PointsDocument,Guid>(configuration)
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddSingleton<IPointsRepository,PointsRepository>()
            .AddRabbitMQ(configuration)
            .AddScoped<CustomMiddleware>()
            .AddConsul(configuration);

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        => app
            .UseMiddleware<CustomMiddleware>()
            .UseConsul()
            .UseRabbitMQ();
}