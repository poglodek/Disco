using MediatR;

namespace Disco.Service.Users.Application.Commands;

public class DeleteUser : IRequest<Unit>
{
    public Guid Id { get; }
    
    public DeleteUser(Guid id)
    {
        Id = id;
    }
}