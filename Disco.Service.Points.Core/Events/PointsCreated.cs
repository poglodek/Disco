namespace Disco.Service.Points.Core.Events;

public class PointsCreated : IDomainEvent
{
    private readonly Guid _id;

    public PointsCreated(Guid id)
    {
        _id = id;
    }
}