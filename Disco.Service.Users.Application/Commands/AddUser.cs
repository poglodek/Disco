using MediatR;

namespace Disco.Service.Users.Application.Commands;

public sealed class AddUser : IRequest {
    
    public string Email { get; set; } 
    public string Nick { get; set; } 
    public string Password { get; set; }
}