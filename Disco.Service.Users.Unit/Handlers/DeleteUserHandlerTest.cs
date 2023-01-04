using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Commands.Handlers;
using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Events;
using Disco.Service.Users.Core.Exceptions;
using Disco.Service.Users.Core.Repositories;
using Disco.Shared.Rabbit.OutboxPattern.Services;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Unit.Handlers;

public class DeleteUserHandlerTest
{
    private Task Act(DeleteUser request)
        => _handler.Handle(request, CancellationToken.None);

    [Fact]
    public async Task UserNotExist_ShouldThrowAnException()
    {
        var request = new DeleteUser(Guid.NewGuid());
        
        var ex =  await Record.ExceptionAsync(async ()=> await Act(request));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<NotFoundUserException>();
        ((NotFoundUserException) ex).Code.ShouldBe("user_not_found");
        
    }
    
    [Fact]
    public async Task UserIsDeleted_ShouldThrowAnException()
    {
        var guid = Guid.NewGuid();
        var request = new DeleteUser(guid);
        
        var user = new User(new AggregateId(guid), "email@test.com", "Adas", true, DateTime.Today.AddDays(-1), true);
        _userRepository.GetAsync(guid).Returns(user);
        
        var ex =  await Record.ExceptionAsync(async ()=> await Act(request));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<UserAlreadyDeletedException>();
        ((UserAlreadyDeletedException) ex).Code.ShouldBe("user_already_deleted");
        
    }

    [Fact]
    public async Task UserExist_ShouldSoftDeleteUser()
    {
        var guid = Guid.NewGuid();
        var request = new DeleteUser(guid);
        
        var user = new User(new AggregateId(guid), "email@test.com", "Adas", true, DateTime.Today.AddDays(-1), false);
        _userRepository.GetAsync(guid).Returns(user);

        user.Events.Count().ShouldBe(0);
        
        await Act(request);
        
        user.IsDeleted.Value.ShouldBe(true);
        user.Events.Count().ShouldBe(1);
        user.Events.First().ShouldBeOfType<UserDeleted>();

        await _userRepository.Received(1).UpdateAsync(Arg.Any<User>());
        await _processor.Received(1).ProcessAsync(Arg.Any<IEnumerable<IDomainEvent>>());
    }
    


    #region arrange
    
    private readonly IUserRepository _userRepository;
    private readonly DeleteUserHandler _handler;
    private readonly IEventProcessor _processor;
    
    public DeleteUserHandlerTest()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _processor = Substitute.For<IEventProcessor>();
        
        _handler = new DeleteUserHandler(_userRepository,_processor);
    }
    
    
    #endregion
}