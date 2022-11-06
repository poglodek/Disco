using System.Threading.Tasks;
using Disco.Shared.Test.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Disco.Shared.Test.Factories;

public class DiscoAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>, IAsyncLifetime where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("tests");
    }

    public MongoFixture MongoFixture { get; private set; }

    public async Task InitializeAsync()
    {
        MongoFixture = new MongoFixture();
    }

    public async Task DisposeAsync()
    {
        await MongoFixture.DropDatabaseAsync();
    }
}