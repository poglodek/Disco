using Disco.Service.Users.Application.Dto;
using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Repositories;
using Disco.Shared.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Disco.Service.Users.Application.Commands.Handlers;

public class UserLoginHandler : IRequestHandler<UserLoginRequest, JWTokenDto>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<User> _hasher;
    private readonly IJsonWebTokenManager _manager;

    public UserLoginHandler(IUserRepository repository, IPasswordHasher<User> hasher, IJsonWebTokenManager manager)
    {
        _repository = repository;
        _hasher = hasher;
        _manager = manager;
    }
    
    public async Task<JWTokenDto> Handle(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAsyncByEmail(request.Email);
        
        if (user is null)
        {
            throw new NotValidUserEmailAndPasswordException(request.Email);
        }
        
        if (!user.Verified)
        {
            throw new NotVerifiedYetException(user.Id.Value);
        }
        
        var passwordVerificationResult = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            throw new NotValidUserEmailAndPasswordException(request.Email);
        }
        
        
        return _manager.CreateToken(user.Id.Value, user.Email);
        
    }
}