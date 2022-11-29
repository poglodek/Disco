using MediatR;

namespace Disco.Service.Barcodes.Application.Events.External.Handlers;

public class UserVerifiedHandler : INotificationHandler<UserVerified>
{
    public Task Handle(UserVerified request, CancellationToken cancellationToken)
    {
        //TODO: Generate a barcode for user.
        Console.WriteLine(request.UserId + "ID GUID!!");
        return Task.CompletedTask;
    }
}