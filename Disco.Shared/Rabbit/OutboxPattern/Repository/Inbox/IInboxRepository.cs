namespace Disco.Shared.Rabbit.OutboxPattern.Repository.Inbox;

public interface IInboxRepository
{
    Task SaveEvent(Models.Inbox inbox);
    Task<IReadOnlyCollection<Models.Inbox>> ReadUnProcessed();
    Task ProcessSuccessfully(Guid id);
    Task SaveError(Guid id,string error);
}