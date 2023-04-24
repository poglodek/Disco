namespace Disco.Service.Discounts.Core.Exceptions;

public class InvalidPointsValueException : DomainException
{
    public InvalidPointsValueException(int message) : base($"Invalid points value: {message}")
    {
    }

    public override string Code => "invalid_points";
}