namespace Disco.Service.Users.Core.Exceptions;

public class InvalidUserEmailException : DomainException
{
    public InvalidUserEmailException(string email) : base($"Invalid User Email {email}")
    {
    }


    public override string Code
        => "invalid_user_email";
}