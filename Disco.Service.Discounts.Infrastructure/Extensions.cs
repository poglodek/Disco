using Disco.Service.Discounts.Application.Services;
using Disco.Service.Discounts.Core.Repositories;
using Disco.Service.Discounts.Infrastructure.Middleware;
using Disco.Service.Discounts.Infrastructure.Mongo.Documents;
using Disco.Service.Discounts.Infrastructure.Mongo.Repositories;
using Disco.Service.Discounts.Infrastructure.Services;
using Disco.Shared.Consul;
using Disco.Shared.Mongo;
using Disco.Shared.Rabbit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Discounts.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddMongo<DiscountDocument,Guid>(configuration)
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddSingleton<IDiscountRepository,DiscountRepository>()
            .AddSingleton<IBarcodeService,BarcodeService>()
            .AddSingleton<IPointsService,PointsService>()
            .AddRabbitMQ(configuration)
            .AddScoped<CustomMiddleware>()
            .AddConsul(configuration);

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        => app
            .UseMiddleware<CustomMiddleware>()
            .UseConsul()
            .UseRabbitMQ();
}