namespace Disco.Service.Points.Core.Exceptions;

public class NotEnoughPointsException : DomainException
{
    public NotEnoughPointsException(Guid id) : base($"Not enough points with id {id}")
    {
        
    }

    public override string Code => "points_are_not_enough";
}