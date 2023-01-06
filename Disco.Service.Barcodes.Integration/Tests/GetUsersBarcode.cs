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

public class GetUsersBarcode : IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(Guid id)
        => _client.GetAsync($"GetUsersBarcode/{id}");

    [Fact]
    public async Task GetUserIdByBarcode_WithInvalidCode_ShouldReturn400()
    {
        var response = await Act(Guid.Empty);

        var obj = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        obj.ShouldBe("barcode_not_found");
    }
    
    [Fact]
    public async Task GetUserIdByBarcode_WithUserWhoNotExists_ShouldReturn400()
    {
        var response = await Act(Guid.NewGuid());

        var obj = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        obj.ShouldBe("barcode_not_found");
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
        
        var response = await Act(barcode.UserId);

        var obj = await ContentHelper.ReturnObjectFromContent<UsersBarcodeDto>(response);
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        obj.Barcode.ShouldBe(barcode.Code);
    }
    
    
    
    
    #region Arrange
    
    private readonly HttpClient _client;
    private readonly BarCodeFixture BarCodeFixture;
    
    public GetUsersBarcode(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        BarCodeFixture = new BarCodeFixture(factory.MongoFixture);
    }
    
    #endregion
}