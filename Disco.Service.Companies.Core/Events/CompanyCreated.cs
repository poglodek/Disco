namespace Disco.Service.Core.Events;

public class CompanyCreated : IDomainEvent
{
    public Guid Id { get; }

    public CompanyCreated(Guid id)
    {
        Id = id;
    }
}