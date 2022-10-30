using Disco.Shared.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Disco.Shared.Rabbit.OutboxPattern.Models;

public class Outbox : IIdentifiable<Guid> 
{
    public Outbox(Guid id, string eventType, DateTime date, string obj, DateTime? publishedDate = null, bool published = false)
    {
        if (id.Equals(Guid.Empty))
        {
            id = Guid.NewGuid();
        }
        
        Id = id;
        EventType = eventType;
        Date = date;
        Obj = obj;
        PublishedDate = publishedDate;
        Published = published;
    }
    
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } 
    public string EventType { get; set; }
    public DateTime Date { get; set; }
    public string Obj { get; set; }
    public DateTime? PublishedDate { get; set; }
    public bool Published { get; set; } = false;



}