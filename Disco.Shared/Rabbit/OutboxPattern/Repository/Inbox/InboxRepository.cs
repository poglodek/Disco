using Disco.Shared.Mongo.Repository;

namespace Disco.Shared.Rabbit.OutboxPattern.Repository.Inbox;

public class InboxRepository : IInboxRepository
{
    private readonly IMongoRepository<Models.Inbox, Guid> _repository;

    public InboxRepository(IMongoRepository<Models.Inbox,Guid> repository)
    {
        _repository = repository;
        _repository.ChangeCollection("in-box");
    }
    public Task SaveEvent(Models.Inbox inbox)
    {
        return _repository.AddAsync(inbox);
    }

    public Task<IReadOnlyCollection<Models.Inbox>> ReadUnProcessed()
    {
        return _repository.FindAsync(x => !x.Processed && !x.Failed);
    }

    public async Task ProcessSuccessfully(Guid id)
    {
        var inbox = await _repository.GetAsync(x=>x.Id.Equals(id));
        if (inbox is null)
        {
            return;
        }

        inbox.Processed = true;
        inbox.ProcessedDate = DateTime.Now;

        await _repository.UpdateAsync(inbox);
    }

    public async Task SaveError(Guid id, string error)
    {
        var inbox = await _repository.GetAsync(x=>x.Id.Equals(id));
        if (inbox is null)
        {
            return;
        }

        inbox.Failed = true;
        inbox.Error = error;

        await _repository.UpdateAsync(inbox);
    }
}