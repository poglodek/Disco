namespace Disco.Service.Points.Application.Exceptions;

public class UserDoesntExistException : AppException
{
    public UserDoesntExistException(Guid msg) : base($"user with id: {msg} doesn't have points!")
    {
    }

    public override string Code => "user_doesnt_have_points";
}