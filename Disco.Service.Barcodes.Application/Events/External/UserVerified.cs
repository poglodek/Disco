using Disco.Shared.Rabbit.Messages.Consumer;
using MediatR;

namespace Disco.Service.Barcodes.Application.Events.External;

[Message("disco-users")]
public class UserVerified : INotification
{
    public Guid UserId { get; set; }
}