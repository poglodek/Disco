using Disco.Shared.Rabbit.Exceptions;

namespace Disco.Shared.Rabbit.Messages.Consumer;

public class MessageAttribute : Attribute
{
    public string Exchange { get; }

    public MessageAttribute(string exchange)
    {
        if (string.IsNullOrWhiteSpace(exchange))
        {
            throw new InvalidExchangeException("Exchange cannot be null or empty");
        }
        Exchange = exchange;
    }
}