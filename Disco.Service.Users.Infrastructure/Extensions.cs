using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Infrastructure.Mongo.Documents;
using Disco.Service.Users.Infrastructure.Mongo.Repositories;
using Disco.Shared.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddMongo<UserDocument,Guid>(configuration)
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddSingleton<IUserRepository,UserRepository>();
}