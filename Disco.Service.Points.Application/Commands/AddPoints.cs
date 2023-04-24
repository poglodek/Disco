using MediatR;

namespace Disco.Service.Points.Application.Commands;

public class AddPoints : IRequest<Unit>
{
    public AddPoints(Guid pointsId, int points)
    {
        PointsId = pointsId;
        Points = points;
    }

    public Guid PointsId { get; }
    public int Points { get; }
}