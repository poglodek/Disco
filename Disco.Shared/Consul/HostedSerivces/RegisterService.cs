using Disco.Shared.Consul.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Disco.Shared.Consul.HostedSerivces;

public class RegisterService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RegisterService> _logger;

    public RegisterService(IServiceScopeFactory scopeFactory, ILogger<RegisterService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
       using var scope = _scopeFactory.CreateScope();
       _logger.LogInformation($"Registering service in Consul...");
       
       var consul = scope.ServiceProvider.GetRequiredService<IConsulService>();
       var registration = scope.ServiceProvider.GetRequiredService<IConsulRegistration>();
       
       var model = registration.CreateModel();
       
       var response = await consul.Register(model);

       if (response.IsSuccessStatusCode)
       {
           _logger.LogInformation($"Name was registered in Consul successful with id  {model.Name}:{model.Id}");
           return;
       }

       _logger.LogError($"Couldn't register service in Consul {model.Name}:{model.Id}");
       _logger.LogError(await response.Content.ReadAsStringAsync());

    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        _logger.LogInformation($"Unregistering service in Consul...");
       
        var consul = scope.ServiceProvider.GetRequiredService<IConsulService>();
        var registration = scope.ServiceProvider.GetRequiredService<IConsulRegistration>();
       
        var id = registration.ReturnId();
       
        var response = await consul.UnRegister(id);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation($"Name was unregistered in Consul successful with id {id}");
            return;
        }

        _logger.LogError($"Couldn't unregister service in Consul with id:{id}");
    }
}