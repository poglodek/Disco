using Disco.Service.Points.Application.Exceptions;
using Disco.Service.Points.Core.Repositories;
using Disco.Shared.Rabbit.OutboxPattern.Services;
using MediatR;

namespace Disco.Service.Points.Application.Commands.Handlers;

public class AddPointsHandler : IRequestHandler<AddPoints,Unit>
{
    private readonly IPointsRepository _repository;
    private readonly IEventProcessor _eventProcessor;

    public AddPointsHandler(IPointsRepository repository,IEventProcessor eventProcessor)
    {
        _repository = repository;
        _eventProcessor = eventProcessor;
    }
    
    public async Task<Unit> Handle(AddPoints request, CancellationToken cancellationToken)
    {
        var points = await _repository.GetByIdAsync(request.PointsId);

        if (points is null)
        {
            throw new UserDoesntExistException(request.PointsId);
        }

        points.AddPoints(request.Points);

        await _repository.UpdateAsync(points);

        await _eventProcessor.ProcessAsync(points.Events);
        
        return Unit.Value;
    }
}