using Disco.Service.Barcodes.Core.Entities;
using Disco.Service.Barcodes.Core.Repositories;
using Disco.Shared.Rabbit.OutboxPattern.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Disco.Service.Barcodes.Application.Events.External.Handlers;

public class UserVerifiedHandler : INotificationHandler<UserVerified>
{
    private readonly IBarcodeRepository _repository;
    private readonly ILogger<UserVerifiedHandler> _logger;
    private readonly IEventProcessor _eventProcessor;

    public UserVerifiedHandler(IBarcodeRepository repository, ILogger<UserVerifiedHandler> logger, IEventProcessor eventProcessor)
    {
        _repository = repository;
        _logger = logger;
        _eventProcessor = eventProcessor;
    }
    public async Task Handle(UserVerified request, CancellationToken cancellationToken)
    {
        var barcode = Barcode.Create(Guid.NewGuid(), request.UserId);
        
        _logger.LogInformation($"Creating new Barcode for user id: {barcode.UserId}, code :{barcode.Code.Value}");

        await _repository.SaveBarCode(barcode);
        
        await _eventProcessor.ProcessAsync(barcode.Events);
    }
}