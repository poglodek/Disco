using Disco.Service.Users.Application.Dto;
using Disco.Shared.Auth;
using MediatR;

namespace Disco.Service.Users.Application.Commands;

public class UserLoginRequest : IRequest<JWTokenDto>
{
    public string Email { get; init; }
    public string Password { get; init; }
}