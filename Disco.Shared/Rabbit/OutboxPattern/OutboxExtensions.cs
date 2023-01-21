using Disco.Shared.Mongo;
using Disco.Shared.Rabbit.OutboxPattern.BackgroundServices;
using Disco.Shared.Rabbit.OutboxPattern.Models;
using Disco.Shared.Rabbit.OutboxPattern.Repository;
using Disco.Shared.Rabbit.OutboxPattern.Repository.Inbox;
using Disco.Shared.Rabbit.OutboxPattern.Repository.Outbox;
using Disco.Shared.Rabbit.OutboxPattern.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Shared.Rabbit.OutboxPattern;

public static class OutboxExtensions
{
    public static IServiceCollection AddOutboxPattern(this IServiceCollection service, IConfiguration configuration)
    {
        var options = configuration.GetSection("outbox").Get<OutboxOptions>();
        service.AddSingleton(_ => options);
        
        
        
        service.AddMongo<Outbox, Guid>(configuration);
        service.AddMongo<Inbox, Guid>(configuration);
        
        service.AddSingleton<IInboxRepository, InboxRepository>();
        service.AddSingleton<IOutboxRepository, OutboxRepository>();
        
        service.AddTransient<IEventProcessor, EventProcessor>();
        
        service.AddHostedService<BackgroundPublisherService>();
        service.AddHostedService<BackgroundProcessService>();
        
        return service;
    }
}