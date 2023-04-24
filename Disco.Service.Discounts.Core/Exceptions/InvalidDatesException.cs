namespace Disco.Service.Discounts.Core.Exceptions;

public class InvalidDatesException : DomainException
{
    public InvalidDatesException(DateOnly dateStart, DateOnly endDate) : base($"Invalid dates: {dateStart} and {endDate}")
    {
    }

    public override string Code => "invalid_dates";
}