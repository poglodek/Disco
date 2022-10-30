namespace Disco.Shared.Rabbit.OutboxPattern.Repository.Outbox;

public interface IOutboxRepository
{
    Task SaveEvent(Models.Outbox outbox);
    Task<IReadOnlyCollection<Models.Outbox>> ReadUnPublished();
    Task PublishedSuccessfully(Guid id);
    
    


}