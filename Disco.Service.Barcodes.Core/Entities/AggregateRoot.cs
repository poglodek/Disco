using Disco.Service.Barcodes.Core.Events;

namespace Disco.Service.Barcodes.Core.Entities;

public class AggregateRoot
{
    private readonly List<IDomainEvent> _events = new();
    public IEnumerable<IDomainEvent> Events => _events;

    public AggregateId Id { get; protected set; }
    public int Version { get; protected set; }

    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any()) Version++;

        _events.Add(@event);
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}