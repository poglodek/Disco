using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Commands.CommandValidators;
using Disco.Service.Users.Application.Commands.Handlers;
using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Exceptions;
using Disco.Service.Users.Core.Repositories;
using Disco.Service.Users.Infrastructure.Mongo.Documents;
using Disco.Service.Users.Infrastructure.Mongo.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Shouldly;
using Xunit;
using ValidationException = Disco.Service.Users.Application.Exceptions.ValidationException;

namespace Disco.Service.Users.Unit.Handlers;

public class AddUserHandlerTest
{
    private Task Act(AddUser request)
        => _handler.Handle(request, default);

    [Fact]
    public async Task Handle_UserNotExist_NotShouldThrowAnException()
    {
        var request = new AddUser
        {
            Email = "test@mail.com",
            Password = "password12345",
            Nick = "Paul"
        };

        _hasher.HashPassword(Arg.Any<User>(), Arg.Any<string>()).Returns("ASDASDASDASDASDASDASDASDASD");
        _userRepository.ExistsByEmailAsync(request.Email).Returns(false);
        
        var ex = await Record.ExceptionAsync(async () =>await Act(request));

        await _userRepository.Received(1).AddAsync(Arg.Any<User>());
        
        ex.ShouldBeNull();

    }
    [Fact]
    public async Task Handle_UserNotExistWithInvalidNick_ShouldThrowAnException()
    {
        var request = new AddUser
        {
            Email = "test@mail.com",
            Password = "password12345"
        };

        _hasher.HashPassword(Arg.Any<User>(), Arg.Any<string>()).Returns("ASDASDASDASDASDASDASDASDASD");
        _userRepository.ExistsByEmailAsync(request.Email).Returns(false);
        
        var ex = await Record.ExceptionAsync(async () =>await Act(request));

        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidNickException>();
        ((InvalidNickException) ex).Code.ShouldBe("invalid_nick");
        

    }
    
    [Fact]
    public async Task Handle_UserNotExist_CannotHashPassword_ShouldThrowAnException()
    {
        var request = new AddUser
        {
            Email = "test@mail.com",
            Password = "password12345",
            Nick = "Paul"
        };

        var ex = await Record.ExceptionAsync(async () =>await Act(request));

        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidUserPasswordException>();

    }
    
    
    [Fact]
    public async Task Handle_UserNotExist_NotValidUser_ShouldThrowAnException()
    {
        var request = new AddUser
        {
            Email = "testmail.com",
            Password = "pa"
        };

        var ex = await Record.ExceptionAsync(async () =>await Act(request));

        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<ValidationException>();
        ((ValidationException) ex).Code.ShouldBe("validation_error");
        ((ValidationException) ex).Errors.Count.ShouldBe(2);

    }
    
    [Fact]
    public async Task Handle_UserExist_ShouldThrowAnException()
    {
        var request = new AddUser
        {
            Email = "test@mail.com",
            Password = "password12345"
        };
        
        _hasher.HashPassword(Arg.Any<User>(), Arg.Any<string>()).Returns("ASDASDASDASDASDASDASDASDASD");
        _userRepository.ExistsByEmailAsync(request.Email).Returns(true);
        
        var ex = await Record.ExceptionAsync(async () =>await Act(request));

        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<UserEmailExistException>();
        ((UserEmailExistException) ex).Code.ShouldBe("user_with_this_email_exists");

    }
    
    
    
    #region arrange
    
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _hasher;
    private readonly IValidator<AddUser> _validator;
    private readonly AddUserHandler _handler;
    
    public AddUserHandlerTest()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _hasher = Substitute.For<IPasswordHasher<User>>();
        _validator = new AddUserHandlerValidator();
        
        _handler = new AddUserHandler(_userRepository, _hasher,_validator);
    }
    
    
    #endregion
}