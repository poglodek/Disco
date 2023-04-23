namespace Disco.Service.Points.Application.Exceptions;

public class InvalidUserIdException : AppException
{
    public InvalidUserIdException(Guid msg) : base($"invalid user id: {msg}")
    {
    }

    public override string Code => "invalid_user_id";
}