namespace Disco.Service.Users.Application.Exceptions;

public class NotValidUserEmailAndPasswordException : ApplicationException
{
    public NotValidUserEmailAndPasswordException(string mail) : base($"User with email {mail} and this password not found")
    {
    }
    

    public override string Code
    => "not_valid_user_email_and_password";
}