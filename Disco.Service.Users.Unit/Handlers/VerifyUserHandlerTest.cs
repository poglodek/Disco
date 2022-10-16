using System;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Commands.CommandValidators;
using Disco.Service.Users.Application.Commands.Handlers;
using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Exceptions;
using Disco.Service.Users.Core.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Unit.Handlers;

public class ValidateUserHandlerTest
{
    private Task Act(Guid guid)
        => _handler.Handle(new VerifyUser {Id = guid},default);
    
    [Fact]
    public async Task Handle_UserExistsWithOutVerifiedStatus()
    {
        var guid = Guid.NewGuid();
        
        _userRepository.GetAsync(guid).Returns(ReturnUser(false,guid));

        var ex = await Record.ExceptionAsync(async () => await Act(guid));
        
        ex.ShouldBeNull();

        await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
    }
    
    [Fact]
    public async Task Handle_UserExistsWithVerifiedStatus_ShouldThrowAnException()
    {
        var guid = Guid.NewGuid();
        
        _userRepository.GetAsync(guid).Returns(ReturnUser(true,guid));

        var ex = await Record.ExceptionAsync(async () => await Act(guid));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<UserAlreadyVerifiedException>();
        ((UserAlreadyVerifiedException)ex).Code.ShouldBe("user_already_verified");
    }
    [Fact]
    public async Task Handle_UserNotExists_ShouldThrowAnException()
    {
        var guid = Guid.NewGuid();

        var ex = await Record.ExceptionAsync(async () => await Act(guid));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<NotFoundUserException>();
        ((NotFoundUserException)ex).Code.ShouldBe("user_not_found");
    }
    
    
    #region arrange

    private User ReturnUser(bool verified, Guid guid)
        => new User(new AggregateId(guid), "email@test.com", verified, DateTime.Now);

    private readonly IUserRepository _userRepository;
    private readonly VerifyUserHandler _handler;
    
    public ValidateUserHandlerTest()
    {
        _userRepository = Substitute.For<IUserRepository>();

        _handler = new VerifyUserHandler(_userRepository);
    }
    
    
    #endregion
}