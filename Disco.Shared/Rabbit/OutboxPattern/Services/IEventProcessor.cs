namespace Disco.Shared.Rabbit.OutboxPattern.Services;

public interface IEventProcessor
{
    Task ProcessAsync<T>(IEnumerable<T> @events) where T : class;
}