using Disco.Service.Points.Application.Dto;
using MediatR;

namespace Disco.Service.Points.Application.Commands;

public class GetPointsByUserId : IRequest<PointsDto>
{
    public Guid Id { get; }

    public GetPointsByUserId(Guid id)
    {
        Id = id;
    }
}