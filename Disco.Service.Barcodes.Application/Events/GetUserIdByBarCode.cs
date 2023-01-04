using Disco.Service.Barcodes.Application.Dto;
using Disco.Service.Barcodes.Core.Expcetions;
using Disco.Service.Barcodes.Core.ValueObjects;
using MediatR;

namespace Disco.Service.Barcodes.Application.Events;

public class GetUserIdByBarCode : IRequest<UserIdDto>
{
    public long Id { get; private set; }
    public GetUserIdByBarCode(long id)
    {
        Id = id;
    }
}