using System;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Disco.Service.Discounts.Application.Dto;
using Disco.Service.Discounts.Integration.Fixtures;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Fixtures;
using Disco.Shared.Test.Helpers;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Disco.Service.Discounts.Integration.Tests;

public class UseDiscounts : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(Application.Commands.UseDiscounts model)
        => _client.PostAsync($"UseDiscounts", ContentHelper.GetContent(model));


    [Fact]
    public async Task UseDiscounts_DiscountAndBarCodeValid_Return200()
    {
        var guidDiscount = Guid.NewGuid();
        var userId = Guid.NewGuid();
        long barcode = 34343434343434;
        
        
        await _discountFixture.AddDiscount(guidDiscount, "Super", 5, 5, Guid.NewGuid(), DateTime.Now,
            DateTime.Now.AddDays(1));

        var model = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = ContentHelper.GetContent(new UserIdDto(userId))
        };
        
        _clientFixture.GetAsync(Arg.Any<string>()).Returns(model);

        var response = await Act(new Application.Commands.UseDiscounts(barcode, guidDiscount));
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

    }
    
    #region Arrange
    
    private readonly HttpClient _client;
    private readonly DiscountFixture _discountFixture;
    private readonly FabioHttpClientFixture _clientFixture;
    
    
    public UseDiscounts(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _clientFixture = factory.HttpClientFixture;

        _discountFixture = new DiscountFixture(factory.MongoFixture);
    }
    
    #endregion
}