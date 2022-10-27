using System;
using System.Threading;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Commands.CommandValidators;
using Disco.Service.Users.Application.Commands.Handlers;
using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Repositories;
using Disco.Shared.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Unit.Handlers;

public class LoginUserHandlerTest
{
    private Task<JWTokenDto> Act(UserLoginRequest request)
        => _handler.Handle(request, CancellationToken.None);

    [Fact]
    public async Task UserDoesNotExist_Should_ThrowAnException()
    {
        var request = new UserLoginRequest
        {
            Email = "none@email.com",
            Password = "gacol12355"
        };

        var ex = await Record.ExceptionAsync(async () => await Act(request));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<NotValidUserEmailAndPasswordException>();
        
        ((NotValidUserEmailAndPasswordException) ex).Code.ShouldBe("not_valid_user_email_and_password");

    }
    
    [Fact]
    public async Task UserExistsWithBadPassword_Should_ThrowAnException()
    {
        _userRepository.GetAsyncByEmail(Arg.Any<string>()).Returns(ReturnUser(Guid.NewGuid()));
        _hasher.VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Failed);
        
        var request = new UserLoginRequest
        {
            Email = "none@email.com",
            Password = "gacol12355"
        };

        var ex = await Record.ExceptionAsync(async () => await Act(request));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<NotValidUserEmailAndPasswordException>();
        
        ((NotValidUserEmailAndPasswordException) ex).Code.ShouldBe("not_valid_user_email_and_password");

    }
    
    [Fact]
    public async Task UserExistsWithValidPasswordButNotVerified_Should_ThrowAnException()
    {
        _userRepository.GetAsyncByEmail(Arg.Any<string>()).Returns(ReturnUser(Guid.NewGuid(),false));
        _hasher.VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Success);
        
        var request = new UserLoginRequest
        {
            Email = "none@email.com",
            Password = "gacol12355"
        };

        var ex = await Record.ExceptionAsync(async () => await Act(request));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<NotVerifiedYetException>();
        
        ((NotVerifiedYetException) ex).Code.ShouldBe("user_not_verified_yet");

    }
    
    
    [Fact]
    public async Task UserExistsWithValidPasswordVerified_ShouldNot_ThrowAnException()
    {
        _userRepository.GetAsyncByEmail(Arg.Any<string>()).Returns(ReturnUser(Guid.NewGuid()));
        _hasher.VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Success);
        
        var request = new UserLoginRequest
        {
            Email = "none@email.com",
            Password = "gacol12355"
        };

        var ex = await Record.ExceptionAsync(async () => await Act(request));
        
        ex.ShouldBeNull();

    }
    
    [Fact]
    public async Task UserExistsWithValidPasswordVerified_Should_ReturnAToken()
    {
        var guid = Guid.NewGuid();
        _userRepository.GetAsyncByEmail(Arg.Any<string>()).Returns(ReturnUser(guid));
        _hasher.VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Success);
        _jsonWebTokenManager.CreateToken(guid, Arg.Any<string>()).Returns(new JWTokenDto
        {
            UserId = guid,
            ExpiresInHours = 8,
            JWT = "token"
        });
        
        
        var request = new UserLoginRequest
        {
            Email = "none@email.com",
            Password = "gacol12355"
        };

        var result = await Act(request);

        request.ShouldNotBeNull();
        result.UserId.ShouldBe(guid);
        result.ExpiresInHours.ShouldBe(8);
        result.JWT.ShouldBe("token");

    }
    
    
    #region arrange
    
    private User ReturnUser(Guid id,bool verified = true)
        => new User(new AggregateId(id), "email@test.com", "Paul",verified, DateTime.Now);    
    
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _hasher;
    private readonly UserLoginHandler _handler;
    private readonly IJsonWebTokenManager _jsonWebTokenManager;
    
    
    public LoginUserHandlerTest()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _hasher = Substitute.For<IPasswordHasher<User>>();
        _jsonWebTokenManager = Substitute.For<IJsonWebTokenManager>();
        
        _handler = new UserLoginHandler(_userRepository, _hasher,_jsonWebTokenManager);
    }
    
    
    #endregion
}