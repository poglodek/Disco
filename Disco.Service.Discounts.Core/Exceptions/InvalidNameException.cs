namespace Disco.Service.Discounts.Core.Exceptions;

public class InvalidNameException : DomainException
{
    public InvalidNameException(string message) : base($"Name is invalid: {message}")
    {
    }

    public override string Code => "invalid_discount_name";
}