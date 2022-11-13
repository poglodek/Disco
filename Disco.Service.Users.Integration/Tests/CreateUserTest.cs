using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Users.Application.Commands;
using Disco.Service.Users.Integration.Helpers;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Helpers;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Integration.Tests;

public class CreateUserTest : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(AddUser user)
        => _client.PostAsync("AddUser", ContentHelper.GetContent(user));
    
    
    [Fact]
    public async Task CreateUser_WithNullBody_ShouldReturn400()
    {
        var result = await Act(null);

        result.IsSuccessStatusCode.ShouldBeFalse();
    }

    [Fact]
    public async Task CreateUser_ValidModel_ShouldReturn200()
    {
        var request = new AddUser
        {
            Email = "super@mail.com",
            Nick = "Super nickname",
            Password = "SecretPassword123"
        };
        
        var result = await Act(request);
        
        result.IsSuccessStatusCode.ShouldBeTrue();

    }
    
    [Fact]
    public async Task CreateUser_ValidModelButEmailIsUsed_ShouldReturn200()
    {
        var request = new AddUser
        {
            Email = "super2@mail.com",
            Nick = "Super nickname",
            Password = "SecretPassword123"
        };
        
        await Act(request);
        var result = await Act(request);

        var text = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<string>(text);
        result.IsSuccessStatusCode.ShouldBeFalse();
        obj.ShouldBe("user_with_this_email_exists");
        
    }
    
    
    #region Arrange
    
    private readonly HttpClient _client;
    
    public CreateUserTest(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    #endregion
}