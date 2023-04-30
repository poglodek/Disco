using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Discounts.Integration.Fixtures;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Fixtures;
using Disco.Shared.Test.Helpers;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Disco.Service.Discounts.Integration.Tests;

public class AddDiscount : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(Application.Commands.AddDiscount model)
        => _client.PostAsync($"AddDiscount", ContentHelper.GetContent(model));


    [Fact]
    public async Task AddDiscount_ValidModel_Return200()
    {
       var model = ReturnValidModel();

       var response = await Act(model);

       response.StatusCode.ShouldBe(HttpStatusCode.OK);
       
    }
    
    [Fact]
    public async Task AddDiscount_NegativePercent_Return400()
    {
        var model = new Application.Commands.AddDiscount(Guid.NewGuid(), -15, 250, "Super new",
            DateTime.Now, DateTime.Now.AddDays(10));
        
        var response = await Act(model);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var error = await ContentHelper.ReturnObjectFromContent<string>(response);
        error.ShouldBe("invalid_percent");

    }
    
    [Fact]
    public async Task AddDiscount_NegativePoints_Return400()
    {
        var model = new Application.Commands.AddDiscount(Guid.NewGuid(), 15, -250, "Super new",
            DateTime.Now, DateTime.Now.AddDays(10));
        
        var response = await Act(model);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        var error = await ContentHelper.ReturnObjectFromContent<string>(response);
        error.ShouldBe("invalid_points");
       
    }

    [Fact]
    public async Task AddDiscount_InvalidName_Return400()
    {
        var model = new Application.Commands.AddDiscount(Guid.NewGuid(), 15, 250, "",
            DateTime.Now, DateTime.Now.AddDays(10));
        
        var response = await Act(model);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        var error = await ContentHelper.ReturnObjectFromContent<string>(response);
        error.ShouldBe("invalid_discount_name");
       
    }
    
    [Fact]
    public async Task AddDiscount_InvalidDates_Return400()
    {
        var model = new Application.Commands.AddDiscount(Guid.NewGuid(), 15, 250, "Super",
            DateTime.Now, DateTime.Now.AddDays(-10));
        
        var response = await Act(model);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        var error = await ContentHelper.ReturnObjectFromContent<string>(response);
        error.ShouldBe("invalid_dates");
       
    }
    
    [Fact]
    public async Task AddDiscount_InvalidCompanyId_Return400()
    {
        var model = new Application.Commands.AddDiscount(Guid.Empty, 15, 250, "Super",
            DateTime.Now, DateTime.Now.AddDays(-0));
        
        var response = await Act(model);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await ContentHelper.ReturnObjectFromContent<string>(response);
        error.ShouldBe("invalid_company_id");
       
    }
    
    private Application.Commands.AddDiscount ReturnValidModel() =>
        new  Application.Commands.AddDiscount(Guid.NewGuid(), 15, 250, "Super new",
            DateTime.Now, DateTime.Now.AddDays(10));


    #region Arrange
    
    private readonly HttpClient _client;
    private readonly DiscountFixture _discountFixture;
    private readonly FabioHttpClientFixture _clientFixture;
    
    
    public AddDiscount(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _clientFixture = factory.HttpClientFixture;

        _discountFixture = new DiscountFixture(factory.MongoFixture);
    }
    
    #endregion
}