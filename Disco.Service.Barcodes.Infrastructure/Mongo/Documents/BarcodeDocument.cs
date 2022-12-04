using Disco.Shared.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Disco.Service.Barcodes.Infrastructure.Mongo.Documents;


public class BarcodeDocument : IIdentifiable<Guid >
{
    [BsonRepresentation(BsonType.String)]
    public Guid  Id { get; init; }
    public long Code { get; init; }
    public Guid UserId { get; init; }
}