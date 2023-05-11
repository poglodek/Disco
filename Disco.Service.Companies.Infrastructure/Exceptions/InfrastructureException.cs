namespace Disco.Service.Companies.Infrastructure.Exceptions;

public abstract class InfrastructureException : Exception
{
    public abstract string Code { get; }

    protected InfrastructureException(string message) : base(message) 
    {
        
    }
}