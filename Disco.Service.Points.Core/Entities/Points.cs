using Disco.Service.Points.Core.Events;
using Disco.Service.Points.Core.Exceptions;
using Disco.Service.Points.Core.ValueObjects;

namespace Disco.Service.Points.Core.Entities;

public class Points : AggregateRoot
{
    public UserId UserId { get; private set; }
    public PointValue PointValue { get; private set; }

    public Points(Guid id,Guid userId, int points)
    {
        if (userId == Guid.Empty)
        {
            throw new InvalidUserIdException(userId);
        }

        if (points < 0)
        {
            throw new InvalidPointsValueException(points);
        }
        
        Id = new AggregateId(id);
        UserId = new UserId(userId);
        PointValue = new PointValue(points);
    }
    
    public static Points Create(Guid id,Guid userId, int points)
    {
        var point = new Points(id,userId, points);
        
        point.AddEvent(new PointsCreated(id));
        
        return point;
    }

    public void AddPoints(int many)
    {
        if (many < 0)
        {
            throw new InvalidPointsOperationException(Id.Value);
        }
        
        var oldPoints = PointValue.Value;
        PointValue = new PointValue(oldPoints + many);
        
        AddEvent(new PointsAdded(Id.Value));
    }

    public void ClearPoints()
    {
        PointValue = new PointValue(0);
        
        AddEvent(new PointsCleared(Id.Value));
    }

    public void SubtractPoints(int value)
    {
        if (value < 0)
        {
            throw new InvalidPointsOperationException(Id.Value);
        }
        
        if (value > PointValue.Value)
        {
            throw new NotEnoughPointsException(Id.Value);
        }
        
        var oldPoints = PointValue.Value;
        PointValue = new PointValue(oldPoints - value);
        
        AddEvent( new PointsSubtracted(Id.Value));
    }
}