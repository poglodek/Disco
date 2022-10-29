namespace Disco.Shared.Rabbit;

public class RabbitOptions
{
    public string? HostName { get; init; }
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public string? Exchange { get; init; }
}