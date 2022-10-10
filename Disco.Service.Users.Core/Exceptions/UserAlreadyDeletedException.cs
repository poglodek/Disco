namespace Disco.Service.Users.Core.Exceptions;

public class UserAlreadyDeletedException : DomainException
{
    public UserAlreadyDeletedException(Guid id) : base($"User is already deleted: {id}")
    {
    }

    public override string Code
        => "user_already_deleted";
}