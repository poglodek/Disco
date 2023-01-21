using Disco.Service.Points.Core.Repositories;
using MediatR;

namespace Disco.Service.Points.Application.Events.External.Handlers;

public class BarcodeHandler : INotificationHandler<BarcodeCreated>
{
    private readonly IPointsRepository _repository;

    public BarcodeHandler(IPointsRepository repository)
    {
        _repository = repository;
    }
    public Task Handle(BarcodeCreated notification, CancellationToken cancellationToken)
    {
        var points = Core.Entities.Points.Create(Guid.NewGuid(), notification.UserId, 0);

        return _repository.AddAsync(points);
    }
}