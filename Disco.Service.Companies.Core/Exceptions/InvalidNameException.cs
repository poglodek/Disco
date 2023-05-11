namespace Disco.Service.Core.Exceptions;

public class InvalidNameException : DomainException
{
    public InvalidNameException(string message) : base($"Invalid name {message}")
    {
    }

    public override string Code => "invalid_name";
}