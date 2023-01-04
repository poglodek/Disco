using Disco.Service.Barcodes.Application.Dto;
using Disco.Service.Barcodes.Application.Exception;
using Disco.Service.Barcodes.Core.Repositories;
using MediatR;

namespace Disco.Service.Barcodes.Application.Events.Handlers;

public class GetUserIdByBarCodeHandler : IRequestHandler<GetUserIdByBarCode, UserIdDto>
{
    private readonly IBarcodeRepository _repository;

    public GetUserIdByBarCodeHandler(IBarcodeRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<UserIdDto> Handle(GetUserIdByBarCode request, CancellationToken cancellationToken)
    {
        var barcode = await _repository.GetByCodeAsync(request.Id);

        if (barcode is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        return new UserIdDto(barcode.UserId.Value);
    }
}