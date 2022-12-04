using Disco.Service.Barcodes.Core.Expcetions;

namespace Disco.Service.Barcodes.Core.Entities;

public class AggregateId : IEquatable<AggregateId>
{
    public Guid Value { get; }
    
    public AggregateId() : this(Guid.NewGuid())
    {
    }

    public AggregateId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidAggregateIdException(value);
        }

        Value = value;
    }
    public bool Equals(AggregateId? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AggregateId) obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    
}