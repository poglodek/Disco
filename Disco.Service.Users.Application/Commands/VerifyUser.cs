using MediatR;

namespace Disco.Service.Users.Application.Commands;

public class VerifyUser : IRequest
{
    public Guid Id { get; set; }
}