using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Application.Services;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ValidationException = Disco.Service.Users.Application.Exceptions.ValidationException;

namespace Disco.Service.Users.Application.Commands.Handlers;

public class AddUserHandler : IRequestHandler<AddUser,Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _hasher;
    private readonly IValidator<AddUser> _validator;
    private readonly IEventProcessor _eventProcessor;

    public AddUserHandler(IUserRepository userRepository, IPasswordHasher<User> hasher, IValidator<AddUser> validator, IEventProcessor eventProcessor)
    {
        _userRepository = userRepository;
        _hasher = hasher;
        _validator = validator;
        _eventProcessor = eventProcessor;
    }
    
    public async Task<Unit> Handle(AddUser request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(request, validationResult.Errors);
        }
        
        if (await _userRepository.ExistsByEmailAsync(request.Email))
        {
            throw new UserEmailExistException(request.Email);
        }
        
        var user =  User.Create(new AggregateId(), request.Email, request.Nick, false, DateTime.Now);

        var hash = _hasher.HashPassword(user, request.Password);
        
        user.SetNewPasswordHash(hash,request.Password);
        
        await _userRepository.AddAsync(user);

        await _eventProcessor.ProcessAsync(user.Events);
        
        return Unit.Value;
    }
}