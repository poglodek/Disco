using MediatR;

namespace Disco.Service.Points.Application.Commands;

public class AddPoints : IRequest<Unit>
{
    public Guid PointsId { get; }
    public int Points { get; }
}