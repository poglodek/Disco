using System;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Application.Commands.CommandValidators;
using Disco.Service.Users.Application.Commands.Handlers;
using Disco.Service.Users.Application.Dto;
using Disco.Service.Users.Application.Queries;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Repositories;
using Disco.Service.Users.Infrastructure.QueryHandlers;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Unit.Handlers;

public class GetUserInformationHandlerTest
{
    private Task<UserDto> Act(Guid id)
        => _handler.Handle(new GetUserInformation(id), default);
    
    [Fact]
    public async Task GetUser_WhenUserExists_ReturnsUserInformation()
    {
        var guid = Guid.NewGuid();
        var dt = DateTime.Now;
        _userRepository.GetAsync(guid).Returns(ReturnNewUser(guid,dt));
        
        var result = await Act(guid);
        
        result.Id.ShouldBe(guid);
        result.Email.ShouldBe("test@email.com");
        result.IsVerified.ShouldBeFalse();
        result.CreatedDate.ShouldBe(dt);
    }
    [Fact]
    public async Task GetUser_WhenUserExistsButIsDeleted_ReturnsNull()
    {
        var guid = Guid.NewGuid();
        var dt = DateTime.Now;
        _userRepository.GetAsync(guid).Returns(ReturnNewUser(guid,dt,true));
        
        var result = await Act(guid);
        
        result.ShouldBeNull();
    }
    [Fact]
    public async Task GetUser_WhenUserNot_ReturnsNull()
    {
        var guid = Guid.NewGuid();

        var result = await Act(guid);
        
        result.ShouldBeNull();
    }




    #region arrange
    
    private User ReturnNewUser(Guid id,DateTime time,bool deleted = false)
    {
        return new User(new AggregateId(id), "test@email.com",  "Paul",false, time,deleted);
    }
    
    
    private readonly IUserRepository _userRepository;
    private readonly GetUserInformationHandler _handler;
    
    public GetUserInformationHandlerTest()
    {
        _userRepository = Substitute.For<IUserRepository>();
   
        _handler = new GetUserInformationHandler(_userRepository);
    }
    
    
    #endregion
}