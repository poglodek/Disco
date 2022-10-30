using Disco.Shared.Rabbit.OutboxPattern.Models;
using Disco.Shared.Rabbit.OutboxPattern.Repository;
using Disco.Shared.Rabbit.OutboxPattern.Repository.Outbox;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Disco.Shared.Rabbit.OutboxPattern.Services;

internal sealed class EventProcessor : IEventProcessor
{
    
    private readonly IOutboxRepository _repository;
    private readonly ILogger<EventProcessor> _logger;

    public EventProcessor(IOutboxRepository repository,ILogger<EventProcessor> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public EventProcessor()
    {
        
    }
    public async Task ProcessAsync<T>(IEnumerable<T> @events) where T : class
    {
        if (!@events.Any())
        {
            _logger.LogInformation($"Cannot process events because list is empty.");
            return;
        }
        
        _logger.LogInformation($"Processing events via outbox ({@events.Count()})...");

        foreach (var @event in @events)
        {
            var text = JsonConvert.SerializeObject(@event);
            await _repository.SaveEvent(new Outbox(Guid.NewGuid(), @event.GetType().Name,DateTime.Now, text));
        }
        
        _logger.LogInformation($"Processing complete.");
    }
}