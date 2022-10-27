namespace Disco.Service.Users.Application.Exceptions;

public class NotVerifiedYetException : ApplicationException
{
    public NotVerifiedYetException(Guid id) : base($"User with id {id} is not verified yet.")
    {
    }

    public override string Code 
    => "user_not_verified_yet";
}