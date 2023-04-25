namespace Disco.Shared.Fabio.HttpClient;

public interface IFabioHttpClient 
{
    Task<HttpResponseMessage> GetAsync(string url, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(string url, StringContent content, CancellationToken ct = default);
    Task<HttpResponseMessage> PatchAsync(string url, StringContent content, CancellationToken ct = default);
    Task<HttpResponseMessage> PatchAsync(string url, object content, CancellationToken ct = default);
    Task<HttpResponseMessage> PutAsync(string url, object content, CancellationToken ct = default);
    Task<HttpResponseMessage> PutAsync(string url, StringContent content, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(string url, object content, CancellationToken ct = default);
    Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken ct = default);
}