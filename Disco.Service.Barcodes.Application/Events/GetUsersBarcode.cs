using Disco.Service.Barcodes.Application.Dto;
using MediatR;

namespace Disco.Service.Barcodes.Application.Events;

public class GetUsersBarcode : IRequest<UsersBarcodeDto>
{
    public Guid UserId { get; }

    public GetUsersBarcode(Guid userId)
    {
        UserId = userId;
    }
}