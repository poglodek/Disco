using Disco.Shared.Mongo.Repository;

namespace Disco.Shared.Rabbit.OutboxPattern.Repository.Outbox;

public class OutboxRepository : IOutboxRepository 
{
    private readonly IMongoRepository<Models.Outbox, Guid> _repository;

    public OutboxRepository(IMongoRepository<Models.Outbox,Guid> repository)
    {
        _repository = repository;
        _repository.ChangeCollection("out-box");
    }
    public Task SaveEvent(Models.Outbox outbox)
    {
        return _repository.AddAsync(outbox);
    }
    
    public  Task<IReadOnlyCollection<Models.Outbox>> ReadUnPublished()
    {
        return _repository.FindAsync(x => !x.Published);
    }

    public async Task PublishedSuccessfully(Guid id)
    {
        var outbox = await _repository.GetAsync(x => x.Id.Equals(id));

        if (outbox is null)
        {
            return;
        }

        outbox.Published = true;
        outbox.PublishedDate = DateTime.Now;
        
        await _repository.UpdateAsync(outbox);
    }
}