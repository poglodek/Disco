using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Users.Integration.Fixtures;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Helpers;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Integration.Tests;

public class DeleteUserTest : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(Guid id)
        => _client.DeleteAsync($"delete/{id.ToString()}");

    [Fact]
    public async Task DeleteUser_UserNotExist_Return400()
    {
        var result = await Act(Guid.NewGuid());
        var obj = await ContentHelper.ReturnObjectFromContent<string>(result);
        
        result.ShouldNotBeNull();
        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        obj.ShouldBe("user_not_found");
        
    }
    
    [Fact]
    public async Task DeleteUser_EmptyGuid_Return400()
    {
        var result = await Act(Guid.NewGuid());
        var obj = await ContentHelper.ReturnObjectFromContent<string>(result);
        
        result.ShouldNotBeNull();
        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        obj.ShouldBe("user_not_found");
    }
    
    [Fact]
    public async Task DeleteUser_UserExist_Return200()
    {
        var guid = Guid.NewGuid();
        await _userFixture.AddUserToDatabase(guid, isDeleted: false);
        
        var result = await Act(guid);
        var obj = await ContentHelper.ReturnObjectFromContent<string>(result);
        
        result.ShouldNotBeNull();
        result.IsSuccessStatusCode.ShouldBeTrue();
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task DeleteUser_UserExistAndIsDeleted_Return400()
    {
        var guid = Guid.NewGuid();
        await _userFixture.AddUserToDatabase(guid, isDeleted: true);
        
        var result = await Act(guid);
        var obj = await ContentHelper.ReturnObjectFromContent<string>(result);
        
        result.ShouldNotBeNull();
        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        obj.ShouldBe("user_already_deleted");
    }
    
    
    #region Arrange

    private readonly HttpClient _client;
    private readonly UserFixture _userFixture;
    
    public DeleteUserTest(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        var db = factory.MongoFixture;
        _userFixture = new UserFixture(db);

    }
    
    #endregion
}