namespace Disco.Service.Barcodes.Core.Expcetions;

public class InvalidUserIdException : DomainException
{
    public InvalidUserIdException(Guid id) : base($"User with id {id} is not valid")
    {
    }
    public InvalidUserIdException(string id) : base($"User with id {id} is not valid")
    {
    }

    public override string Code => "invalid_user_id";
}