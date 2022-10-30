using Disco.Shared.Rabbit.Connection;
using Disco.Shared.Rabbit.Exceptions;
using Disco.Shared.Rabbit.Messages;
using Disco.Shared.Rabbit.Messages.Consumer;
using Disco.Shared.Rabbit.Messages.Producer;
using Disco.Shared.Rabbit.OutboxPattern;
using Disco.Shared.Rabbit.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Disco.Shared.Rabbit;

public static class RabbitExtensions
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection service, IConfiguration configuration)
    {
        var rabbitOption = configuration.GetSection("rabbit").Get<RabbitOptions>();

        if (string.IsNullOrWhiteSpace(rabbitOption.Exchange))
        {
            throw new InvalidExchangeException("Exchange cannot be null or empty");
        }
        
        service.AddSingleton(_ => rabbitOption);
        service.AddSingleton<IRabbitConnection,RabbitConnection>();
        service.AddSingleton<IConsumerBinder,ConsumerBinder>();
        
        service.AddSingleton<IAssembliesService,AssembliesService>();
        
        
        service.AddScoped<IMessageProducer, MessageProducer>();
        service.AddOutboxPattern(configuration);
        return service;
    }
    public static IApplicationBuilder UseRabbitMQ(this IApplicationBuilder app)
    {
        var rabbitConnection = app.ApplicationServices.GetService<IRabbitConnection>();
        
        
        rabbitConnection?.Connect();
        rabbitConnection?.ExchangeDeclare();
        
        var binder = app.ApplicationServices.GetService<IConsumerBinder>();
        
        binder?.BindAllConsumers();
        
        return app;
    }
}