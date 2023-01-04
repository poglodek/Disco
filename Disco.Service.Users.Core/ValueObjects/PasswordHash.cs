using Disco.Service.Users.Core.Exceptions;

namespace Disco.Service.Users.Core.ValueObjects;

public struct PasswordHash
{
    public string Value { get; private set; }

    private PasswordHash(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
    public void ValidatePassword(string password)
    {
        if (password is null)
            throw new InvalidUserPasswordException(nameof(password));
        
        if(password.Length < 7)
            throw new InvalidUserPasswordException(password);
        
    }

    public static implicit operator PasswordHash(string value) => new (value);
    public static implicit operator string(PasswordHash value) => value.Value;
}