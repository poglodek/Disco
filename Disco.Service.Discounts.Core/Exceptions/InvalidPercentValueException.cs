namespace Disco.Service.Discounts.Core.Exceptions;

public class InvalidPercentValueException : DomainException
{
    public InvalidPercentValueException(int percent) : base($"Invalid percent: {percent}%")
    {
    }

    public override string Code => "invalid_percent";
}