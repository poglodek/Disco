namespace Disco.Service.Users.Application.Exceptions;

public class NotFoundUserException : ApplicationException
{
    public NotFoundUserException(Guid msg) : base($"User not found with guid: {msg.ToString()}")
    {
    }

    public override string Code 
        => "user_not_found";
}