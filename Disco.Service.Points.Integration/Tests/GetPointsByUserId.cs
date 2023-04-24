using System;
using System.Net.Http;
using System.Threading.Tasks;
using Disco.Service.Points.Application.Dto;
using Disco.Service.Points.Integration.Fixtures;
using Disco.Shared.Test.Factories;
using Disco.Shared.Test.Helpers;
using Shouldly;
using Xunit;

namespace Disco.Service.Points.Integration.Tests;

public class GetPointsByUserId: IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(Guid id)
        => _client.GetAsync($"GetPointsByUserId/{id}");

    [Fact]
    public async Task GetPoints_EmptyGuid_ShouldReturn400()
    {
        var id = Guid.Empty;

        var response = await Act(id);

        response.IsSuccessStatusCode.ShouldBeFalse();

        var content = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        content.ShouldBe("points_not_found");
    }
    
    [Fact]
    public async Task GetPoints_GuidDoesntExist_ShouldReturn400()
    {
        var id = Guid.NewGuid();

        var response = await Act(id);

        response.IsSuccessStatusCode.ShouldBeFalse();

        var content = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        content.ShouldBe("points_not_found");
    }
    
    [Fact]
    public async Task GetPoints_PointsExistsButOnAnotherUser_ShouldReturn400()
    {
        var id  = Guid.NewGuid();

        await _pointsFixture.AddPoints(id, 100, Guid.NewGuid());    
        
        var response = await Act(id);

        response.IsSuccessStatusCode.ShouldBeFalse();

        var content = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        content.ShouldBe("points_not_found");
    }
    
    [Fact]
    public async Task GetPoints_PointsExists_ShouldReturn200()
    {
        var pointsId = Guid.NewGuid();
        var id  = Guid.NewGuid();

        await _pointsFixture.AddPoints(pointsId, 100, id);    
        
        var response = await Act(id);

        response.IsSuccessStatusCode.ShouldBeTrue();

        var content = await ContentHelper.ReturnObjectFromContent<PointsDto>(response);
        
        content!.Points.ShouldBe(100);
        content!.UserId.ShouldBe(id);
        content!.Id.ShouldBe(pointsId);
    }
    





    #region Arrange
    
    private readonly HttpClient _client;
    private readonly PointsFixture _pointsFixture;
    
    public GetPointsByUserId(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _pointsFixture = new PointsFixture(factory.MongoFixture);
    }
    
    #endregion
}