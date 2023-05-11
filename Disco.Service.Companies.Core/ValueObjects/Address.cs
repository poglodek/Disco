using Disco.Service.Core.Exceptions;

namespace Disco.Service.Core.ValueObjects;

public record Address
{
    public string Value { get; }

    public Address(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidAddressException(value);
        }
        
        Value = value;
    }
}