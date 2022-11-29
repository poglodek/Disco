using Disco.Shared.Rabbit.Messages.Consumer;
using MediatR;

namespace Disco.Service.Barcodes.Application.Events.External;
[Message("disco-user")]
public class AddUser : INotification
{
    public string Email { get; set; } 
    public string Nick { get; set; } 
    public string Password { get; set; }
}