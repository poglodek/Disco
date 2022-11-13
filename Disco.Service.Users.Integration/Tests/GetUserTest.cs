using System;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Dto;
using Disco.Service.Users.Integration.Fixtures;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Helpers;
using MongoDB.Bson;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Integration.Tests;

public class GetUserTest : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(Guid id)
        => _client.GetAsync($"Get/{id.ToString()}");

    [Fact]
    public async Task GetUser_UserNotExists_ShouldReturn400()
    {
        var result = await Act(Guid.NewGuid());

        var obj = await ContentHelper.ReturnObjectFromContent<string>(result);

        result.ShouldNotBeNull();
        result.IsSuccessStatusCode.ShouldBeFalse();
        obj.ShouldBe("user_not_found");

    }
    
    
    [Fact]
    public async Task GetUser_UserIsDeleted_ShouldReturn400()
    {
        var guid = Guid.NewGuid();
        await _userFixture.AddUserToDatabase(guid, true, true);
        
        var result = await Act(guid);

        var obj = await ContentHelper.ReturnObjectFromContent<string>(result);

        result.ShouldNotBeNull();
        result.IsSuccessStatusCode.ShouldBeFalse();
        obj.ShouldBe("user_not_found");

    }
    
    [Fact]
    public async Task GetUser_UserExists_ShouldReturn200()
    {
        var guid = Guid.NewGuid();
        await _userFixture.AddUserToDatabase(guid, true, false);
        
        var result = await Act(guid);

        var obj = await ContentHelper.ReturnObjectFromContent<UserDto>(result);

        result.ShouldNotBeNull();
        result.IsSuccessStatusCode.ShouldBeTrue();
        obj!.IsVerified.ShouldBeTrue();
        obj!.Id.ShouldBe(guid);

    }
    
    [Fact]
    public async Task GetUser_InvalidGuid_ShouldReturn400()
    {
        var result = await Act(Guid.Empty);

        var obj = await ContentHelper.ReturnObjectFromContent<string>(result);

        result.ShouldNotBeNull();
        result.IsSuccessStatusCode.ShouldBeFalse();
        obj.ShouldBe("invalid_guid_id");

    }
    
    #region Arrange

    private readonly HttpClient _client;
    private readonly UserFixture _userFixture;
    
    public GetUserTest(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        var db = factory.MongoFixture;
        _userFixture = new UserFixture(db);

    }
    
    #endregion
}