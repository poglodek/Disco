using System.Reflection;
using System.Text;
using System.Text.Json;
using Disco.Shared.Rabbit.Connection;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Type = System.Type;

namespace Disco.Shared.Rabbit.Messages.Consumer;

public class ConsumerBinder : IConsumerBinder
{
    private readonly RabbitOptions _options;
    private readonly IModel _channel;
    private readonly IMediator _mediator;
    private readonly ILogger<ConsumerBinder> _logger;
    private IEnumerable<Type> _assemblies;

    public ConsumerBinder(IRabbitConnection connection, RabbitOptions options,IMediator mediator, ILogger<ConsumerBinder> logger)
    {
        _options = options;
        _mediator = mediator;
        _logger = logger;
        _channel = connection.Channel;
    }
    public void BindAllConsumers()
    {
        var consumers = GetConsumersFromAssembly();
        var exchange = _options.Exchange;

        _assemblies = GetConsumersFromAssembly();
        
        _logger.LogInformation($"Bind consumers ({consumers.Count()}) to exchange {exchange}");
        
        foreach (var consumerType in _assemblies)
        {
            var key = consumerType.Name;
            var queueName = _channel.QueueDeclare().QueueName;
            
            _channel.QueueBind(queueName,exchange,key);
            
            _logger.LogInformation($"Queue bind to exchange {exchange} with key {key}");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += ConsumerOnReceived;
            
            _channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);
            
            _logger.LogInformation($"Channel is now consuming messages from {exchange} with key {key}");
        }

        
    }

    private void ConsumerOnReceived(object? sender, BasicDeliverEventArgs @event)
    {
        var body = @event.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        
        var type = _assemblies.FirstOrDefault(x => x.Name == @event.RoutingKey);
        
        
        _logger.LogInformation($"Recived message with key {type.Name}");
        
        var request = JsonSerializer.Deserialize(message,type);
        
        if (request is not null)
        {
            _mediator.Publish(request);    
            _logger.LogInformation($"Recived message with key {type.Name} handled successfully");
        }
    }
    

    private IEnumerable<Type> GetConsumersFromAssembly()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();
        files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));
        
        return assemblies
            .SelectMany(x => x.GetTypes())
            .Where(t => typeof(INotification).IsAssignableFrom(t) && t.GetCustomAttributes(typeof(MessageAttribute),true).Length > 0);
    }
}