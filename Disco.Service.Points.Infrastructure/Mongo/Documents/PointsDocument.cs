using Disco.Shared.Mongo;

namespace Disco.Service.Points.Infrastructure.Mongo.Documents;


public class PointsDocument : IIdentifiable<Guid >
{
    public Guid  Id { get; set; }
    public Guid  UserId { get; set; }
    public int Points { get; set; }

}