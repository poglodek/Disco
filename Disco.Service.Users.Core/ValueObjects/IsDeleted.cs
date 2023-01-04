namespace Disco.Service.Users.Core.ValueObjects;

public struct IsDeleted
{
    public bool Value { get; private set; }

    private IsDeleted(bool value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator IsDeleted(bool value) => new (value);
    public static implicit operator bool(IsDeleted value) => value.Value;
}