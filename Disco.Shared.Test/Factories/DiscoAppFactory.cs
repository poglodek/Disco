using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Shared.Fabio.HttpClient;
using Disco.Shared.Test.Fixtures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Xunit;

namespace Disco.Shared.Test.Factories;

public class DiscoAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>, IAsyncLifetime where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("tests");
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IAuthorizationHandler, AllowAnonymous>();
            services.AddHttpClient<IFabioHttpClient, FabioHttpClientFixture>(x=> HttpClientFixture);
            
        });
        
        return base.CreateHost(builder);
    }

    public readonly FabioHttpClientFixture HttpClientFixture = Substitute.For<FabioHttpClientFixture>(); 

    public MongoFixture MongoFixture { get; private set; }

    public Task InitializeAsync()
    {
        MongoFixture = new MongoFixture();
        
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await MongoFixture.DropDatabaseAsync();
    }
}
