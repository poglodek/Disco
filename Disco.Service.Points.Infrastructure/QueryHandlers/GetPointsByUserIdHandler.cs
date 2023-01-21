using AutoMapper;
using Disco.Service.Points.Application.Commands;
using Disco.Service.Points.Application.Dto;
using Disco.Service.Points.Core.Repositories;
using Disco.Service.Points.Infrastructure.Exceptions;
using MediatR;

namespace Disco.Service.Points.Infrastructure.QueryHandlers;

public class GetPointsByUserIdHandler : IRequestHandler<GetPointsByUserId,PointsDto>
{
    private readonly IPointsRepository _repository;
    private readonly IMapper _mapper;

    public GetPointsByUserIdHandler(IPointsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<PointsDto> Handle(GetPointsByUserId request, CancellationToken cancellationToken)
    {
        var points = await _repository.GetByUserIdAsync(request.Id);

        if (points is null)
        {
            throw new PointsNotFoundExceptions(request.Id);
        }

        return _mapper.Map<PointsDto>(points);
    }
}