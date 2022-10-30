namespace Disco.Shared.Rabbit.OutboxPattern;

public class OutboxOptions
{
    public string? PublishTimeSpan { get; set; }
    public string? ProcessTimeSpan { get; set; }
}