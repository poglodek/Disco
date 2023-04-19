using Disco.Shared.Fabio.HttpClient;
using Disco.Shared.Fabio.HttpHandler;
using Disco.Shared.Fabio.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Shared.Fabio;

public static class FabioExtensions
{
    public static IServiceCollection AddFabio(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var options = configuration.GetSection("fabio").Get<FabioOptions>();
        serviceCollection.AddSingleton(_ => options);
        
        serviceCollection.AddHttpClient<IFabioHttpClient, FabioHttpClient>(c =>
        {
            c.Timeout = TimeSpan.Parse(options!.TimeSpan);
            
        }).AddHttpMessageHandler<FabioHttpClientHandler>();
        
        return serviceCollection;
    }
    
}