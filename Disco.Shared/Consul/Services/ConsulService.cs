using Disco.Shared.Consul.HttpClient;
using Disco.Shared.Consul.Models;

namespace Disco.Shared.Consul.Services;

public class ConsulService : IConsulService
{
    private readonly ConsulHttpClient _httpClient;

    public ConsulService(ConsulHttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public Task<HttpResponseMessage> Register(ConsulRegistrationModel model)
    {
        return _httpClient.Register(model);
    }

    public Task<HttpResponseMessage> UnRegister(Guid id)
    {
        return _httpClient.UnRegister(id);
    }
}