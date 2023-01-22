using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Repositories;
using Disco.Service.Users.Infrastructure.Middleware;
using Disco.Service.Users.Infrastructure.Mongo.Documents;
using Disco.Service.Users.Infrastructure.Mongo.Repositories;
using Disco.Shared.Consul;
using Disco.Shared.Mongo;
using Disco.Shared.Rabbit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddMongo<UserDocument,Guid>(configuration)
            .AddRabbitMQ(configuration)
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddSingleton<IUserRepository,UserRepository>()
            .AddScoped<CustomMiddleware>()
            .AddConsul(configuration);
    
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        => app
            .UseMiddleware<CustomMiddleware>()
            .UseRabbitMQ()
            .UseConsul();
}