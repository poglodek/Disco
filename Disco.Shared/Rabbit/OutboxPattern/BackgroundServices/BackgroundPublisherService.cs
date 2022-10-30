using Disco.Shared.Rabbit.Messages.Producer;
using Disco.Shared.Rabbit.OutboxPattern.Models;
using Disco.Shared.Rabbit.OutboxPattern.Repository;
using Disco.Shared.Rabbit.OutboxPattern.Repository.Outbox;
using Disco.Shared.Rabbit.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;

namespace Disco.Shared.Rabbit.OutboxPattern.BackgroundServices;

public class BackgroundPublisherService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly ILogger<BackgroundPublisherService> _logger;
    private readonly IOutboxRepository _repository;
    private IEnumerable<Type> _types;
    private readonly IMessageProducer _producer;
    private readonly PeriodicTimer _timer;
    private readonly AsyncRetryPolicy _retryPolicy;
    

    public BackgroundPublisherService( ILogger<BackgroundPublisherService> logger, IServiceProvider serviceProvider, OutboxOptions options)
    {
        _logger = logger;
        using var scope = serviceProvider.CreateScope();
        
        _repository = scope.ServiceProvider.GetService<IOutboxRepository>()!;
        _producer = scope.ServiceProvider.GetService<IMessageProducer>()!;

        _timer = new PeriodicTimer(TimeSpan.Parse(options.PublishTimeSpan!));
        _types =  scope.ServiceProvider.GetService<IAssembliesService>()!.ReturnTypes();

       _retryPolicy = Policy.Handle<Exception>().RetryAsync(3);
       
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       
        while ( await _timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                var @events  = await _repository.ReadUnPublished();

                if (!@events.Any())
                {
                    continue;
                }
            
                _logger.LogInformation($"Found {@events.Count} outbox messages, publishing on service bus...");
            
                foreach (var @event in events.Take(10))
                {
                   await _retryPolicy.ExecuteAsync(async () =>  await Publish(@event));
                }
                
                _logger.LogInformation($"Publishing complete.");
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception during publishing a message {e.Message}");
            }
        }
    }

    private async Task Publish(Outbox @event)
    {
        var type = _types.FirstOrDefault(x => x.Name == @event.EventType);
        
        if (type is null)
        {
            _logger.LogError($"Cannot find type with name {@event.EventType}");
            return;
        }
        
        await _producer.PublishAsync(JsonConvert.DeserializeObject(@event.Obj, type));
        await _repository.PublishedSuccessfully(@event.Id);
    }
}