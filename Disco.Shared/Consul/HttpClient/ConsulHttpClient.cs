using System.Text;
using System.Text.Json;
using Disco.Shared.Consul.Models;

namespace Disco.Shared.Consul.HttpClient;

public class ConsulHttpClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public ConsulHttpClient(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<HttpResponseMessage> Register(ConsulRegistrationModel model)
        => _httpClient.PutAsync("agent/service/register", GetPayload(model));
    
    public Task<HttpResponseMessage> UnRegister(Guid id)
        => _httpClient.PutAsync($"agent/service/deregister/{id}", EmptyPayload());

    private static StringContent EmptyPayload()
        => GetPayload(new { });


    private static StringContent GetPayload(object obj)
    {
        return new( JsonSerializer.Serialize(obj), Encoding.UTF8,"application/json");
    }
}