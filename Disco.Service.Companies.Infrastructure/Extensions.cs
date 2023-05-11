using Disco.Service.Companies.Infrastructure.Mongo.Documents;
using Disco.Service.Companies.Infrastructure.Mongo.Repositories;
using Disco.Service.Core.Repositories;
using Disco.Shared.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Service.Companies.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
        => serviceCollection
            .AddMongo<CompanyDocument, Guid>(configuration)
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddScoped<CustomMiddleware>()
            .AddSingleton<ICompanyRepository,CompanyRepository>();
}

