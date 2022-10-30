namespace Disco.Shared.Rabbit.Messages.Producer;

public interface IMessageProducer
{
    Task PublishAsync<T>(T message) where T : class?;
}