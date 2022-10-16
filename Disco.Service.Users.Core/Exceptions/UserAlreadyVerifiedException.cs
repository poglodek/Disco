namespace Disco.Service.Users.Core.Exceptions;

public class UserAlreadyVerifiedException : DomainException
{
    public UserAlreadyVerifiedException(Guid guid) : base($"User is already verified with {guid.ToString()}")
    {
    }

    public override string Code
        => "user_already_verified";
}