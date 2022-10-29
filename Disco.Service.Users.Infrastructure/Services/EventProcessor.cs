using Disco.Service.Users.Application.Services;
using Disco.Service.Users.Core.Events;
using Disco.Shared.Rabbit.Messages.Producer;
using Microsoft.Extensions.Logging;

namespace Disco.Service.Users.Infrastructure.Services;

public class EventProcessor : IEventProcessor
{
    private readonly IMessageProducer _producer;
    private readonly ILogger<EventProcessor> _logger;

    public EventProcessor(IMessageProducer producer,ILogger<EventProcessor> logger)
    {
        _producer = producer;
        _logger = logger;
    }
    public async Task ProcessAsync(IEnumerable<IDomainEvent> @events)
    {
        if (!@events.Any())
        {
            _logger.LogInformation($"Cannot process events because list is empty.");
            return;
        }
        
        _logger.LogInformation($"Processing events ({@events.Count()})...");

        foreach (var @event in @events)
        {
            await _producer.PublishAsync(@event);
        }
        
        _logger.LogInformation($"Processing complete.");
    }
}