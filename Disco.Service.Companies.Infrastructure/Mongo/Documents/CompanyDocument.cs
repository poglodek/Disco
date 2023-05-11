using Disco.Shared.Mongo;

namespace Disco.Service.Companies.Infrastructure.Mongo.Documents;

public class CompanyDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}