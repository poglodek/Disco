using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Barcodes.Application.Dto;
using Disco.Service.Barcodes.Infrastructure.Mongo.Documents;
using Disco.Service.Barcodes.Integration.Fixtures;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Fixtures;
using Disco.Shared.Test.Helpers;
using Shouldly;
using Xunit;

namespace Disco.Service.Barcodes.Integration.Tests;

public class GetUserIdByBarcode : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(long id)
        => _client.GetAsync($"GetUserIdByBarcode/{id}");

    [Fact]
    public async Task GetUserIdByBarcode_WithInvalidCode_ShouldReturn400()
    {
        var response = await Act(-500);

        var obj = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        obj.ShouldBe("user_not_found");
    }
    
    [Fact]
    public async Task GetUserIdByBarcode_WithUserWhoNotExists_ShouldReturn400()
    {
        var response = await Act(69420);

        var obj = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        obj.ShouldBe("user_not_found");
    }
    
    [Fact]
    public async Task GetUserIdByBarcode_WithUserWhoExists_ShouldReturn200()
    {
        var barcode = new BarcodeDocument
        {
            Code = BarCodeFixture.GenerateNewCode(),
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        await BarCodeFixture.AddBarcodeToDatabase(barcode);
        
        var response = await Act(barcode.Code);

        var obj = await ContentHelper.ReturnObjectFromContent<UserIdDto>(response);
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        obj.Id.ShouldBe(barcode.UserId);
    }
    
    
    
    
    #region Arrange
    
    private readonly HttpClient _client;
    private readonly BarCodeFixture BarCodeFixture;
    
    public GetUserIdByBarcode(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        BarCodeFixture = new BarCodeFixture(factory.MongoFixture);
    }
    
    #endregion
}