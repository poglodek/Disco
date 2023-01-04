using Disco.Service.Barcodes.Core.Entities;
using Disco.Service.Barcodes.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Disco.Service.Barcodes.Application.Events.External.Handlers;

public class UserVerifiedHandler : INotificationHandler<UserVerified>
{
    private readonly IBarcodeRepository _repository;
    private readonly ILogger<UserVerifiedHandler> _logger;

    public UserVerifiedHandler(IBarcodeRepository repository, ILogger<UserVerifiedHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public Task Handle(UserVerified request, CancellationToken cancellationToken)
    {
        var barcode = Barcode.Create(Guid.NewGuid(), request.UserId);
        
        _logger.LogInformation($"Creating new Barcode for user id: {barcode.UserId}, code :{barcode.Code.Value}");
        
        return _repository.SaveBarCode(barcode);
    }
}