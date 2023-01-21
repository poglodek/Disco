namespace Disco.Service.Points.Application.Exceptions;

public abstract class AppException : Exception
{
    protected AppException(string msg) : base(msg)
    {
        
    }
    public abstract string Code { get; }
}