using Disco.Service.Users.Application.Dto;
using MediatR;

namespace Disco.Service.Users.Application.Queries;

public class GetUserInformation : IRequest<UserDto>
{
    public GetUserInformation(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }
}