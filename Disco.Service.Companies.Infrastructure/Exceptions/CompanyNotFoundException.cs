namespace Disco.Service.Companies.Infrastructure.Exceptions;

public class CompanyNotFoundException : InfrastructureException
{
    public CompanyNotFoundException(Guid id) : base($"Company with id: {id} was not found.")
    {
    }

    public override string Code => $"company_not_found";
}