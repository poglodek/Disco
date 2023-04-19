using System.Text;
using System.Text.Json;

namespace Disco.Shared.Fabio.HttpClient;

public class FabioHttpClient : IFabioHttpClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public FabioHttpClient(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<HttpResponseMessage> GetAsync(string url, CancellationToken ct = default)
        => _httpClient.GetAsync(url, ct);

    public Task<HttpResponseMessage> PostAsync(string url, StringContent content, CancellationToken ct = default)
        => _httpClient.PostAsync(url, content, ct);

    public Task<HttpResponseMessage> PatchAsync(string url, StringContent content, CancellationToken ct = default)
        => _httpClient.PatchAsync(url, content, ct);

    public Task<HttpResponseMessage> PatchAsync(string url, object content, CancellationToken ct = default)
        => _httpClient.PatchAsync(url, GetStringContent(content), ct);

    public Task<HttpResponseMessage> PostAsync(string url, object content, CancellationToken ct = default)
        => _httpClient.PostAsync(url, GetStringContent(content), ct);

    public Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken ct = default)
        => _httpClient.DeleteAsync(url, ct);
    
    private StringContent GetStringContent(object obj) => new StringContent( JsonSerializer.Serialize(obj), Encoding.UTF8,"application/json");
}