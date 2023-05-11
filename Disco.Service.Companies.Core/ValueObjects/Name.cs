using Disco.Service.Core.Exceptions;

namespace Disco.Service.Core.ValueObjects;

public record Name
{
    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidNameException(value);
        }
        
        Value = value;
    }
};