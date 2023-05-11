namespace Disco.Service.Core.Exceptions;

public class InvalidAddressException : DomainException
{
    public InvalidAddressException(string message) : base($"Invalid address {message}")
    {
    }

    public override string Code => "invalid_address";
}