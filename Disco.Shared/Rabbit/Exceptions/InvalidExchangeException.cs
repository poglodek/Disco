namespace Disco.Shared.Rabbit.Exceptions;

public class InvalidExchangeException : Exception
{
    public InvalidExchangeException(string msg) : base(msg)
    {
        
    }
}