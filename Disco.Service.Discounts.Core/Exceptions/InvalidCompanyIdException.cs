namespace Disco.Service.Discounts.Core.Exceptions;

public class InvalidCompanyIdException : DomainException
{
    public InvalidCompanyIdException(Guid message) : base($"Invalid company id: {message}")
    {
    }

    public override string Code => "invalid_company_id";
}