namespace Disco.Service.Users.Core.ValueObjects;

public struct CreatedDate
{
    public DateTime Value { get; private set; }

    private CreatedDate(DateTime value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString("u");
    }

    public static implicit operator CreatedDate(DateTime value) => new (value);
    public static implicit operator DateTime(CreatedDate value) => value.Value;
}