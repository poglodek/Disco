using Disco.Shared.Consul.Models;
using Disco.Shared.Consul.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Shared.Consul.Services;

public class ConsulRegistration : IConsulRegistration
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly Guid _id = Guid.NewGuid();
    public ConsulRegistration(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public ConsulRegistrationModel CreateModel()
    {
        using var scope = _scopeFactory.CreateScope();
        var options = scope.ServiceProvider.GetRequiredService<ConsulOptions>();

        return new ConsulRegistrationModel
        {
            Address = options.ServiceAddress,
            Name = options.Name,
            Id = _id,
            Port = options.Port,
            Tags = options.Tags
        };
    }

    public Guid ReturnId()
        => _id;
}