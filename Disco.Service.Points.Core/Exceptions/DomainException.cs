namespace Disco.Service.Points.Core.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }

    public abstract string Code { get; }
}