namespace Disco.Service.Points.Core.Events;

public class PointsSubtracted : IDomainEvent
{
    private readonly Guid _id;

    public PointsSubtracted(Guid id)
    {
        _id = id;
    }
}