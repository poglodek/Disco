namespace Disco.Service.Users.Core.Exceptions;

public class InvalidNickException : DomainException
{
    public InvalidNickException(string message) : base(message)
    {
    }

    public override string Code    
        => "invalid_nick";
}