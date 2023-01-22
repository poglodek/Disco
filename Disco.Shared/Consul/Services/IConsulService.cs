using Disco.Shared.Consul.Models;

namespace Disco.Shared.Consul.Services;

public interface IConsulService
{
    Task<HttpResponseMessage> Register(ConsulRegistrationModel model);
    Task<HttpResponseMessage> UnRegister(Guid id);
}