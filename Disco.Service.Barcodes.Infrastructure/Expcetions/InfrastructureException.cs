namespace Disco.Service.Barcodes.Infrastructure.Expcetions;

public abstract class InfrastructureException : Exception
{
    protected InfrastructureException(string message) : base(message)
    {
    }

    public abstract string Code { get; }
}