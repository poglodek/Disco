using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Infrastructure.Mongo.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Disco.Service.Users.Application.Commands.Handlers;

public class AddUserHandler : IRequestHandler<AddUser,Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _hasher;

    public AddUserHandler(IUserRepository userRepository, IPasswordHasher<User> hasher)
    {
        _userRepository = userRepository;
        _hasher = hasher;
    }
    
    public async Task<Unit> Handle(AddUser request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsAsync(x => x.Email.Equals(request.Email)))
        {
            throw new UserEmailExistException(request.Email);
        }

        var user =  new User(new AggregateId(), request.Email, false, DateTime.Now);

        var hash = _hasher.HashPassword(user, request.Password);
        
        user.SetNewPasswordHash(hash,request.Password);
        
        await _userRepository.AddAsync(user);
        
        return Unit.Value;
    }
}