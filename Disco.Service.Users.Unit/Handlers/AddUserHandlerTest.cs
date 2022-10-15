using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Commands.Handlers;
using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Exceptions;
using Disco.Service.Users.Infrastructure.Mongo.Documents;
using Disco.Service.Users.Infrastructure.Mongo.Repositories;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Unit.Handlers;

public class AddUserHandlerTest
{
    private Task Act(AddUser request)
        => _handler.Handle(request, default);

    [Fact]
    public async Task Hanlde_UserNotExist_NotShouldThrowAnException()
    {
        var request = new AddUser
        {
            Email = "test@mail.com",
            Password = "password12345"
        };

        _hasher.HashPassword(Arg.Any<User>(), Arg.Any<string>()).Returns("ASDASDASDASDASDASDASDASDASD");
        _userRepository.ExistsAsync(x => x.Email.Equals(request.Email)).Returns(true);
        
        var ex = await Record.ExceptionAsync(async () =>await Act(request));

        ex.ShouldBeNull();

    }
    
    [Fact]
    public async Task Hanlde_UserNotExist_CannotHashPassword_ShouldThrowAnException()
    {
        var request = new AddUser
        {
            Email = "test@mail.com",
            Password = "password12345"
        };

        var ex = await Record.ExceptionAsync(async () =>await Act(request));

        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidUserPasswordException>();

    }
    [Fact]
    public async Task Hanlde_UserExist_ShouldThrowAnException()
    {
        var request = new AddUser
        {
            Email = "test@mail.com",
            Password = "password12345"
        };
        
        _hasher.HashPassword(Arg.Any<User>(), Arg.Any<string>()).Returns("ASDASDASDASDASDASDASDASDASD");
        _userRepository.ExistsAsync(Arg.Any<Expression<Func<UserDocument,bool>>>()).Returns(true);
        
        var ex = await Record.ExceptionAsync(async () =>await Act(request));

        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<UserEmailExistException>();
        ((UserEmailExistException) ex).Code.ShouldBe("user_with_this_email_exists");

    }
    
    
    
    #region arrange
    
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _hasher;
    private readonly AddUserHandler _handler;
    
    public AddUserHandlerTest()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _hasher = Substitute.For<IPasswordHasher<User>>();
        _handler = new AddUserHandler(_userRepository, _hasher);
    }
    
    
    #endregion
}