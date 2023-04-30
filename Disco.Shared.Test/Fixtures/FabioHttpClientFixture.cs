using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Disco.Shared.Fabio.HttpClient;

namespace Disco.Shared.Test.Fixtures;

public class FabioHttpClientFixture : IFabioHttpClient
{
    public Task<HttpResponseMessage> GetAsync(string url, CancellationToken ct = default)
    {
        return Task.FromResult<HttpResponseMessage>(default);
    }

    public Task<HttpResponseMessage> PostAsync(string url, StringContent content, CancellationToken ct = default)
    {
        return Task.FromResult<HttpResponseMessage>(default);
    }

    public Task<HttpResponseMessage> PatchAsync(string url, StringContent content, CancellationToken ct = default)
    {
        return Task.FromResult<HttpResponseMessage>(default);
    }

    public Task<HttpResponseMessage> PatchAsync(string url, object content, CancellationToken ct = default)
    {
        return Task.FromResult<HttpResponseMessage>(default);
    }

    public Task<HttpResponseMessage> PutAsync(string url, object content, CancellationToken ct = default)
    {
        return Task.FromResult<HttpResponseMessage>(default);
    }

    public Task<HttpResponseMessage> PutAsync(string url, StringContent content, CancellationToken ct = default)
    {
        return Task.FromResult<HttpResponseMessage>(default);
    }

    public Task<HttpResponseMessage> PostAsync(string url, object content, CancellationToken ct = default)
    {
        return Task.FromResult<HttpResponseMessage>(default);
    }

    public Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken ct = default)
    {
        return Task.FromResult<HttpResponseMessage>(default);
    }
}