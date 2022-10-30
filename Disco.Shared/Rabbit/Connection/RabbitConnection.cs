using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson.IO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Disco.Shared.Rabbit.Connection;

internal sealed class RabbitConnection : IRabbitConnection
{
    private readonly RabbitOptions _options;
    private IConnection? _connection;
    public IModel? Channel { get; private set; }

    public RabbitConnection(RabbitOptions options)
    {
        _options = options;
       }
    
    public void Connect()
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.HostName, 
            UserName = _options.UserName, 
            Password = _options.Password,
            DispatchConsumersAsync = true
            
        };
        
        _connection = factory.CreateConnection();
        Channel = _connection.CreateModel();
    }

    public void ExchangeDeclare()
    {
        Channel?.ExchangeDeclare(_options.Exchange, ExchangeType.Topic, true, false, null);
    }
    public Task PublishAsync(byte[] obj, string key)
    {
        Channel.BasicPublish(_options.Exchange,key,basicProperties:null, body:obj);
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        _connection?.Dispose();
        Channel?.Dispose();
    }
}