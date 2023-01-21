using Disco.Shared.Rabbit.Messages.Consumer;
using MediatR;

namespace Disco.Service.Points.Application.Events.External;

[Message("disco-barcodes")]

public class BarcodeCreated : INotification 
{
    public Guid UserId { get; }

    public BarcodeCreated(Guid userId)
    {
        UserId = userId;
    }
}