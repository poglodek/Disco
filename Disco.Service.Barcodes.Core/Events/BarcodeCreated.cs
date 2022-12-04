using Disco.Service.Barcodes.Core.Entities;

namespace Disco.Service.Barcodes.Core.Events;

public class BarcodeCreated : IDomainEvent 
{
    public Guid BarcodeId { get; }

    public BarcodeCreated(Guid barcodeId)
    {
        BarcodeId = barcodeId;
    }
}