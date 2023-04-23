namespace Disco.Service.Points.Application.Dto;

public record PointsDto
{
    public Guid  Id { get; set; }
    public Guid  UserId { get; set; }
    public int Points { get; set; }
}