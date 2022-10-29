namespace Disco.Service.Users.Infrastructure.Exceptions;

public class UserNotFoundException : InfrastructureException
{
    public UserNotFoundException(Guid id) : base($"User with id {id} not found")
    {
    }

    public override string Code => "user_not_found";
}