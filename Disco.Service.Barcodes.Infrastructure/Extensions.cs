using Disco.Service.Barcodes.Application.Repositories;
using Disco.Service.Barcodes.Infrastructure.Mongo.Documents;
using Disco.Service.Barcodes.Infrastructure.Mongo.Repositories;
using Disco.Shared.Mongo;
using Disco.Shared.Rabbit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Barcodes.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddMongo<BarcodeDocument,Guid>(configuration)
            .AddRabbitMQ(configuration)
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddSingleton<IBarcodeRepository,BarcodeRepository>();
    
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        => app
            .UseRabbitMQ();
}