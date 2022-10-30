using Disco.Shared.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Disco.Shared.Rabbit.OutboxPattern.Models;

public class Inbox : IIdentifiable<Guid>
{
    public Inbox(Guid id, string eventType, DateTime date, string obj, DateTime? processedDate = null,
        bool processed = false)
    {
        if (Guid.Empty.Equals(id))
        {
            id = Guid.NewGuid();
        }
        
        Id = id;
        EventType = eventType;
        Date = date;
        Obj = obj;
        ProcessedDate = processedDate;
        Processed = processed;
    }

    [BsonRepresentation(BsonType.String)] public Guid Id { get; set; }
    public string EventType { get; set; }
    public DateTime Date { get; set; }
    public string Obj { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public bool Processed { get; set; } = false;
    public bool Failed { get; set; } = false;
    public string Error { get; set; }


}