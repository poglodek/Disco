namespace Disco.Service.Users.Core.Exceptions;

public class InvalidAggregateIdException : DomainException
{
    public InvalidAggregateIdException(Guid message) : base($"Invalid Aggregate Value: {message.ToString()}")
    {
    }

    public override string Code
        => "invalid_aggregate_id";
}