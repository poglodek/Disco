namespace Disco.Service.Users.Core.Exceptions;

public class InvalidUserPasswordException : DomainException
{
    public InvalidUserPasswordException(string password) : base($"Invalid PasswordHash: {password}")
    {
    }


    public override string Code
        => "invalid_user_password";
}