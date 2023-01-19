namespace Disco.Service.Points.Core.Events;

public class PointsAdded : IDomainEvent
{
    private readonly Guid _id;

    public PointsAdded(Guid id)
    {
        _id = id;
    }
}