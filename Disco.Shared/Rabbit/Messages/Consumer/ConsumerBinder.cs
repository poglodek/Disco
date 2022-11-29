using System.Reflection;
using System.Text;
using Disco.Shared.Rabbit.Connection;
using Disco.Shared.Rabbit.OutboxPattern.Models;
using Disco.Shared.Rabbit.OutboxPattern.Repository.Inbox;
using Disco.Shared.Rabbit.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Type = System.Type;

namespace Disco.Shared.Rabbit.Messages.Consumer;

public class ConsumerBinder : IConsumerBinder
{
    private readonly IInboxRepository _repository;
    private readonly IAssembliesService _assembliesService;
    private readonly IModel _channel;
    private readonly ILogger<ConsumerBinder> _logger;
    private IEnumerable<Type> _assemblies;

    public ConsumerBinder(IRabbitConnection connection, ILogger<ConsumerBinder> logger, 
        IInboxRepository repository, IAssembliesService assembliesService)
    {
        _repository = repository;
        _assembliesService = assembliesService;
        _logger = logger;
        _channel = connection.Channel!;
    }
    public void BindAllConsumers()
    {
        _assemblies = _assembliesService
            .ReturnTypes()
            .Where(t => typeof(INotification).IsAssignableFrom(t) 
                        && t.GetCustomAttributes(typeof(MessageAttribute),true).Length > 0);
        
        
        foreach (var consumerType in _assemblies)
        {
            var key = consumerType.Name;
            var queueName = _channel.QueueDeclare().QueueName;
            var exchange = consumerType.GetCustomAttribute<MessageAttribute>().Exchange;

            _channel.QueueBind(queueName,exchange,key);
            
            _logger.LogInformation($"Queue bind to exchange {exchange} with key {key}");

             var consumer = new AsyncEventingBasicConsumer(_channel);
             consumer.Received += ConsumerOnReceived;

             _channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);

            _logger.LogInformation($"Channel is now consuming messages from {exchange} with key {key}");
        }

        
    }

    private async Task ConsumerOnReceived(object? sender, BasicDeliverEventArgs @event)
    {
        var body = @event.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        
        var type = _assemblies.FirstOrDefault(x => x.Name == @event.RoutingKey);
        
        _logger.LogInformation($"Recived message with key {type.Name}");
        
        if (!string.IsNullOrWhiteSpace(message))
        {
            var inbox = new Inbox(Guid.NewGuid(), type.Name, DateTime.Now, message);
            await _repository.SaveEvent(inbox);
            
            _logger.LogInformation($"Recived message with key {type.Name} save in in-box successfully");
        }
        
    }
    
}