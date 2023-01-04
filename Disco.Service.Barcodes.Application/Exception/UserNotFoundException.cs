namespace Disco.Service.Barcodes.Application.Exception;

public class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(long msg) : base($"User with code:{msg} wasn't found!")
    {
    }

    public override string Code => "user_not_found";
}