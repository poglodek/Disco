using MediatR;

namespace Disco.Service.Points.Application.Commands;

public class SubtractPoints : IRequest<Unit>
{
    public SubtractPoints(Guid pointsId, int points)
    {
        PointsId = pointsId;
        Points = points;
    }

    public Guid PointsId { get;  }
    public int Points { get;  }
}