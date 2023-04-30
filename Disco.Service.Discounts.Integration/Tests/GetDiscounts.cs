using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Discounts.Application.Dto;
using Disco.Service.Discounts.Integration.Fixtures;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Fixtures;
using Disco.Shared.Test.Helpers;
using Shouldly;
using Xunit;

namespace Disco.Service.Discounts.Integration.Tests;

public class GetDiscounts : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act()
        => _client.GetAsync($"GetDiscounts");

    [Fact]
    public async Task GetDiscounts_Should_Return200()
    {
        await _discountFixture.AddDiscount(Guid.NewGuid(), "super", 5, 5, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(10));
        
        var response = await Act();

        var model = await ContentHelper.ReturnObjectFromContent<IEnumerable<DiscountDto>>(response);
        
        model.Count().ShouldBe(1);
    }


    #region Arrange
    
    private readonly HttpClient _client;
    private readonly DiscountFixture _discountFixture;
    private readonly FabioHttpClientFixture _clientFixture;
    
    
    public GetDiscounts(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _clientFixture = factory.HttpClientFixture;

        _discountFixture = new DiscountFixture(factory.MongoFixture);
    }
    
    #endregion
}