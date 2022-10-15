namespace Disco.Service.Users.Application.Exceptions;

public abstract class ApplicationException : Exception
{
    protected ApplicationException(string msg) : base(msg)
    {
        
    }
    public abstract string Code { get; }
}