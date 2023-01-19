namespace Disco.Service.Points.Core.Exceptions;

public class InvalidUserIdException : DomainException
{
    public InvalidUserIdException(Guid id) : base($"User with id {id} is not valid")
    {
    }
    
    public override string Code => "invalid_user_id";
}