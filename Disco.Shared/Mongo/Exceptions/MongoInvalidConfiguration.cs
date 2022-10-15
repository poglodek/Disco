namespace Disco.Shared.Mongo.Exceptions;

public class MongoInvalidConfiguration : Exception
{
    public MongoInvalidConfiguration(string property) : base($"Invalid configuration for property '{property}'")
    { }

}