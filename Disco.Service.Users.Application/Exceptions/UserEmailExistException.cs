namespace Disco.Service.Users.Application.Exceptions;

public class UserEmailExistException : ApplicationException
{
    public UserEmailExistException(string msg) : base($"User with this email {msg} exists")
    {
    }

    public override string Code
        => "user_with_this_email_exists";
}