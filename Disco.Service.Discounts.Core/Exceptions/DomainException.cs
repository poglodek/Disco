namespace Disco.Service.Discounts.Core.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }

    public abstract string Code { get; }
}