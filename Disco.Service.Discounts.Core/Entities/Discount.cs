using Disco.Service.Discounts.Core.ValueObjects;

namespace Disco.Service.Discounts.Core.Entities;

public class Discount : AggregateRoot
{
    public CompanyId Company { get; private set; }
    public Percent Percent { get; private set; }
    public StartedDate StartedDate { get; private set; }
    public EndingDate EndingDate { get; private set; }
}