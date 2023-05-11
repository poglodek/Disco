using Disco.Service.Core.Events;
using Disco.Service.Core.ValueObjects;

namespace Disco.Service.Core.Entities;

public class Company : AggregateRoot
{
    public Name Name { get; private set; }
    public Address Address { get; private set; }
    
    public Company(Guid id, string name, string address)
    {
        Id = new AggregateId(id);
        Name = new Name(name);
        Address = new Address(address);
    }

    public static Company Create(Guid id, string name, string address)
    {
        var company = new Company(id, name, address);
        
        company.AddEvent(new CompanyCreated(company.Id.Value));

        return company;
    }

    public void SetNewAddress(string address) => Address = new Address(address);
    
    
}