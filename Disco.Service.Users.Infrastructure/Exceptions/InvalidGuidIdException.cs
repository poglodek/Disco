namespace Disco.Service.Users.Infrastructure.Exceptions;

public class InvalidGuidIdException : InfrastructureException
{
    public InvalidGuidIdException(Guid id) : base($"Guid {id} is not a valid Guid")
    {
    }
 

    public override string Code => "invalid_guid_id";
}