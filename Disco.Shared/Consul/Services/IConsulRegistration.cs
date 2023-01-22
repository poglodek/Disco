using Disco.Shared.Consul.Models;

namespace Disco.Shared.Consul.Services;

public interface IConsulRegistration
{
    ConsulRegistrationModel CreateModel();
    Guid ReturnId();
}