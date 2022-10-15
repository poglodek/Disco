namespace Disco.Shared.Mongo;

public interface IIdentifiable<out T>
{
    public T Id { get; }
}