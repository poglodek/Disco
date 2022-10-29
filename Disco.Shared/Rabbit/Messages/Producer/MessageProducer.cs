using System.Text;
using System.Text.Json;
using Disco.Shared.Rabbit.Connection;
using Microsoft.Extensions.Logging;

namespace Disco.Shared.Rabbit.Messages.Producer;

internal sealed class MessageProducer : IMessageProducer
{
    private readonly IRabbitConnection _connection;
    private readonly ILogger<MessageProducer> _logger;

    public MessageProducer(IRabbitConnection connection, ILogger<MessageProducer> logger)
    {
        _connection = connection;
        _logger = logger;
    }
    
    public Task PublishAsync<T>(T message) where T : class
    {
        var key = message.GetType().Name;
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        _logger.LogInformation($"Publishing message with key: {key}");
        
        return _connection.PublishAsync(body, key);
    }
}