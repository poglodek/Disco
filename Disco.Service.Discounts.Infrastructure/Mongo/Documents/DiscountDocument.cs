using Disco.Shared.Mongo;

namespace Disco.Service.Discounts.Infrastructure.Mongo.Documents;

public class DiscountDocument :  IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public int Points { get; set; }
    public int Percent { get; set; }
    public string Name { get; set; }
    public DateTime StartedDate { get; set; }
    public DateTime EndingDate { get; set; }
    
}