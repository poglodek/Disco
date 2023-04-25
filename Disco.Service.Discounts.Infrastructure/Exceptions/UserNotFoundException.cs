namespace Disco.Service.Discounts.Infrastructure.Exceptions;

public class UserNotFoundException : InfrastructureException
{
    public UserNotFoundException(long message) : base($"User not found with barcode:{message}")
    {
    }

    public override string Code => "user_not_found";
}