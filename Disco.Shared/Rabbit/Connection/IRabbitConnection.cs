using RabbitMQ.Client;

namespace Disco.Shared.Rabbit.Connection;

public interface IRabbitConnection : IDisposable
{
    Task PublishAsync(byte[] obj, string key);
    public IModel? Channel { get;}
    void Connect();
    void ExchangeDeclare();
    
}