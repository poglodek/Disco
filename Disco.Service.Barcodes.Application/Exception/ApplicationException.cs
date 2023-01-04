namespace Disco.Service.Barcodes.Application.Exception;

public abstract class ApplicationException : System.Exception
{
    protected ApplicationException(string msg) : base(msg)
    {
        
    }
    public abstract string Code { get; }
}