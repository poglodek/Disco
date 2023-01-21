using Disco.Service.Barcodes.Core.Entities;

namespace Disco.Service.Barcodes.Core.Events;

public class BarcodeCreated : IDomainEvent 
{
    public Guid UserId { get; }

    public BarcodeCreated(Guid userId)
    {
        UserId = userId;
    }
}