namespace Disco.Service.Points.Core.Exceptions;

public class InvalidPointsValueException : DomainException
{
    public InvalidPointsValueException(int points) : base($"Points {points} is invalid!")
    {
    }

    public override string Code => "invalid_points";
}