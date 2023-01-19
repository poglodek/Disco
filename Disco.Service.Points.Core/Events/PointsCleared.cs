namespace Disco.Service.Points.Core.Events;

public class PointsCleared : IDomainEvent
{
    private readonly Guid _id;

    public PointsCleared(Guid id)
    {
        _id = id;
    }
}