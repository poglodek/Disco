namespace Disco.Service.Barcodes.Core.Expcetions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }

    public abstract string Code { get; }
}