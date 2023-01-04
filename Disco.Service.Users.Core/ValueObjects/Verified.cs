namespace Disco.Service.Users.Core.ValueObjects;

public struct Verified
{
    public bool Value { get; private set; }

    private Verified(bool value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
    
    

    public static implicit operator Verified(bool value) => new (value);
    public static implicit operator bool(Verified value) => value.Value;
}