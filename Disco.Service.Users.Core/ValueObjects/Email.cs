using Disco.Service.Users.Core.Exceptions;

namespace Disco.Service.Users.Core.ValueObjects;

public struct Email
{
    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
        ValidateEmail(value);
    }
    
    private void ValidateEmail(string email)
    {
        if (email.Length is < 7 or > 30)
            throw new InvalidUserEmailException(email);
        
        if(!email.Contains("@"))
            throw new InvalidUserEmailException(email);
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator Email(string value) => new (value);
    public static implicit operator string(Email value) => value.Value;
}