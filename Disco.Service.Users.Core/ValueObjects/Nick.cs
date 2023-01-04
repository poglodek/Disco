using Disco.Service.Users.Core.Exceptions;

namespace Disco.Service.Users.Core.ValueObjects;

public struct Nick
{
    public string Value { get; private set; }

    private Nick(string value)
    {
        Value = value;
        ValidateNick(value);
    }

    public override string ToString()
    {
        return Value;
    }
    private void ValidateNick(string nick)
    {
        if(string.IsNullOrEmpty(nick))
            throw new InvalidNickException("Nick cannot be empty");
    }

    public static implicit operator Nick(string value) => new (value);
    public static implicit operator string(Nick value) => value.Value;
}