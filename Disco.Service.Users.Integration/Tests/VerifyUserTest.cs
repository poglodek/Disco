using System;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Infrastructure.Mongo.Documents;
using Disco.Service.Users.Integration.Fixtures;
using Disco.Service.Users.Integration.Helpers;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Fixtures;
using Disco.Shared.Test.Helpers;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Integration.Tests;

public class VerifyUserTest : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(VerifyUser user)
        => _client.PutAsync("VerifyUser", ContentHelper.GetContent(user));


    [Fact]
    public async Task VerifyUser_Null_Return400()
    {
        var result =  await Act(null);
            
        result.IsSuccessStatusCode.ShouldBeFalse();

    }
    [Fact]
    public async Task VerifyUser_UserNotExist_Return400()
    {
        var request = new VerifyUser {Id = Guid.NewGuid()};
        var result =  await Act(request);

        var obj = await ContentHelper.ReturnObjectFromContent<string>(result);

        result.IsSuccessStatusCode.ShouldBeFalse();
        obj.ShouldBe("user_not_found");
        
    }

    

    [Fact]
    public async Task VerifyUser_UserExist_Return200()
    {
        var id = Guid.NewGuid();
        var request = new VerifyUser {Id = id};

        await _userFixture.AddUserToDatabase(id);
        
        var result = await Act(request);
        var text = await ContentHelper.ReturnObjectFromContent<string>(result);
        
        result.IsSuccessStatusCode.ShouldBeTrue();
        text.ShouldBeNullOrWhiteSpace();

    }
    
    [Fact]
    public async Task VerifyUser_UserAlreadyVerified_Return400()
    {
        var id = Guid.NewGuid();
        var request = new VerifyUser {Id = id};

        await _userFixture.AddUserToDatabase(id,true);
        
        var result = await Act(request);
        var obj = await ContentHelper.ReturnObjectFromContent<string>(result);
        
        result.IsSuccessStatusCode.ShouldBeFalse();
        obj.ShouldBe("user_already_verified");

    }
    
    #region Arrange

    
    
    private readonly HttpClient _client;
    private readonly UserFixture _userFixture;
    public VerifyUserTest(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        var db = factory.MongoFixture;
        _userFixture = new UserFixture(db);


    }
    
    #endregion
}