namespace Disco.Service.Points.Core.Events;

public class DiscountCreated: IDomainEvent
{
    private readonly Guid _id;

    public DiscountCreated(Guid id)
    {
        _id = id;
    }
}