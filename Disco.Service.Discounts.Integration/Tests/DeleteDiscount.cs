using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Discounts.Integration.Fixtures;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Fixtures;
using Disco.Shared.Test.Helpers;
using Shouldly;
using Xunit;

namespace Disco.Service.Discounts.Integration.Tests;

public class DeleteDiscount : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(Guid id)
        => _client.DeleteAsync($"DeleteDiscount/{id.ToString()}");

    [Fact]
    public async Task Delete_WhenNotExist_Return400()
    {
        var response = await Act(Guid.NewGuid());
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var error = await ContentHelper.ReturnObjectFromContent<string>(response);
        error.ShouldBe("discount_not_found");
    }
    
    [Fact]
    public async Task Delete_EmptyGuid_Return400()
    {
        var response = await Act(Guid.Empty);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var error = await ContentHelper.ReturnObjectFromContent<string>(response);
        error.ShouldBe("discount_not_found");
    }
    
    [Fact]
    public async Task Delete_Exists_Return200()
    {
        var guid = Guid.NewGuid();

        await _discountFixture.AddDiscount(guid, "super", 4, 5, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(1));
        
        var response = await Act(guid);
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

    }
    
    
    #region Arrange
    
    private readonly HttpClient _client;
    private readonly DiscountFixture _discountFixture;
    private readonly FabioHttpClientFixture _clientFixture;
    
    
    public DeleteDiscount(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _clientFixture = factory.HttpClientFixture;

        _discountFixture = new DiscountFixture(factory.MongoFixture);
    }
    
    #endregion
}