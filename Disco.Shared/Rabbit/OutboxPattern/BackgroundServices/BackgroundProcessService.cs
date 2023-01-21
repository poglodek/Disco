using Disco.Shared.Rabbit.Messages.Consumer;
using Disco.Shared.Rabbit.OutboxPattern.Repository;
using Disco.Shared.Rabbit.OutboxPattern.Repository.Inbox;
using Disco.Shared.Rabbit.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ZstdSharp.Unsafe;

namespace Disco.Shared.Rabbit.OutboxPattern.BackgroundServices;

public class BackgroundProcessService : BackgroundService
{
    private readonly ILogger<BackgroundProcessService> _logger;
    private readonly IMediator _mediator;
    private readonly IInboxRepository _repository;
    private readonly IEnumerable<Type> _types;
    private readonly PeriodicTimer _timer;

    public BackgroundProcessService(ILogger<BackgroundProcessService> logger, IServiceProvider serviceProvider, IMediator mediator, OutboxOptions options)
    {
        _logger = logger;
        _mediator = mediator;

        using var scope = serviceProvider.CreateScope();
        
        _types =  scope.ServiceProvider.GetService<IAssembliesService>()!
            .ReturnTypes()
            .Where(t => typeof(INotification).IsAssignableFrom(t) 
            && t.GetCustomAttributes(typeof(MessageAttribute),true).Length > 0);

        _repository = scope.ServiceProvider.GetService<IInboxRepository>()!;
        
        _timer = new PeriodicTimer(TimeSpan.Parse(options.ProcessTimeSpan!));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            var events = await _repository.ReadUnProcessed();

            foreach (var @event in events.Take(7))
            {
                var key = @event.EventType;
                
                _logger.LogInformation($"Processing an event with name {key}");
                
                var type = _types.FirstOrDefault(x => x.Name == key);

                if (type is null)
                {
                    _logger.LogError( $"Cannot find a type with name {key}");
                    await _repository.SaveError(@event.Id, $"Cannot find a type with name {key}");
                    continue;
                }
                
                var obj = JsonConvert.DeserializeObject(@event.Obj, type);
                
                if (obj is null)
                {
                    _logger.LogError( $"Cannot deseralize an object with name {key} and body {@event.Obj}");
                    await _repository.SaveError(@event.Id, $"Cannot deseralize an object with name {key} and body {@event.Obj}");
                    continue;
                }

                try
                {
                    await _mediator.Publish(obj, stoppingToken);
                    
                    await _repository.ProcessSuccessfully(@event.Id);
                
                    _logger.LogInformation($"Process success an event with name {key}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}