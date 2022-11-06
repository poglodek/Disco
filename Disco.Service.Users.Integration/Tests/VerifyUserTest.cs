using System;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Infrastructure.Mongo.Documents;
using Disco.Service.Users.Integration.Helpers;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Fixtures;
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

        var text = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<string>(text);
        
        result.IsSuccessStatusCode.ShouldBeFalse();
        obj.ShouldBe("user_not_found");
        
    }

    [Fact]
    public async Task VerifyUser_UserExist_Return200()
    {
        var id = Guid.NewGuid();
        var request = new VerifyUser {Id = id};

        await AddUserToDatabase(id);
        
        var result = await Act(request);
        var text = await result.Content.ReadAsStringAsync();
        
        result.IsSuccessStatusCode.ShouldBeTrue();
        text.ShouldBeNullOrWhiteSpace();

    }
    
    [Fact]
    public async Task VerifyUser_UserAlreadyVerified_Return400()
    {
        var id = Guid.NewGuid();
        var request = new VerifyUser {Id = id};

        await AddUserToDatabase(id,true);
        
        var result = await Act(request);
        var text = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<string>(text);
        
        result.IsSuccessStatusCode.ShouldBeFalse();
        obj.ShouldBe("user_already_verified");

    }
    
    #region Arrange

    private Task AddUserToDatabase(Guid guid, bool verified = false)
    {
        return _db.GetCollection<UserDocument>().InsertOneAsync(new UserDocument
        {
            Id = guid,
            CreatedDate = DateTime.Now,
            Email = "sample@emial.com",
            IsDeleted = false,
            Nick = "Nickname",
            Verified = verified
        });
    }
    
    private readonly HttpClient _client;
    private readonly MongoFixture _db;
    
    public VerifyUserTest(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _db = factory.MongoFixture;

    }
    
    #endregion
}