using Disco.Service.Barcodes.Application.Dto;
using Disco.Service.Barcodes.Application.Exception;
using Disco.Service.Barcodes.Core.Repositories;
using MediatR;

namespace Disco.Service.Barcodes.Application.Events.Handlers;

public class GetUsersBarcodeHandler : IRequestHandler<GetUsersBarcode,UsersBarcodeDto>
{
    private readonly IBarcodeRepository _barcodeRepository;

    public GetUsersBarcodeHandler(IBarcodeRepository barcodeRepository)
    {
        _barcodeRepository = barcodeRepository;
    }
    public async Task<UsersBarcodeDto> Handle(GetUsersBarcode request, CancellationToken cancellationToken)
    {
        var barcode = await _barcodeRepository.GetByUserIdAsync(request.UserId);

        if (barcode is null)
        {
            throw new BarcodeNotFoundException(request.UserId);
        }

        return new UsersBarcodeDto(barcode.Code.Value);
    }
}