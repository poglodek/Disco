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

public class SubtractPoints: IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(Application.Commands.SubtractPoints model)
        => _client.PutAsync($"SubtractPoints", ContentHelper.GetContent(model));


    [Fact]
    public async Task SubtractPoints_ToPointsDontExist_ShouldReturn400()
    {
        var model = new Application.Commands.SubtractPoints(Guid.NewGuid(), 10);

        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeFalse();

        var content = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        content.ShouldBe("user_doesnt_have_points");

    }
    [Fact]
    public async Task SubtractPoints_ToPointsWithNullId_ShouldReturn400()
    {
        var model = new Application.Commands.SubtractPoints(Guid.Empty, 10);

        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeFalse();

        var content = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        content.ShouldBe("user_doesnt_have_points");

    }
    
    [Fact]
    public async Task SubtractPoints_ToPointsExist_ShouldReturn200()
    {
        var id = Guid.NewGuid();
        var model = new Application.Commands.SubtractPoints(id, 10);

        await _pointsFixture.AddPoints(id, 10, Guid.NewGuid());
        
        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeTrue();


    }
    
    [Fact]
    public async Task SubtractPoints_ToPointsExistButNegativePoints_ShouldReturn400()
    {
        var id = Guid.NewGuid();
        var model = new Application.Commands.SubtractPoints(id, 10);

        await _pointsFixture.AddPoints(id, -10, Guid.NewGuid());
        
        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeFalse();
        var content = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        content.ShouldBe("invalid_points");

    }
    
    [Fact]
    public async Task SubtractPoints_ToPointsExistBut0Points_ShouldReturn400()
    {
        var id = Guid.NewGuid();
        var model = new Application.Commands.SubtractPoints(id, 10);

        await _pointsFixture.AddPoints(id, 0, Guid.NewGuid());
        
        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeFalse();


    }




    #region Arrange
    
    private readonly HttpClient _client;
    private readonly PointsFixture _pointsFixture;
    
    public SubtractPoints(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _pointsFixture = new PointsFixture(factory.MongoFixture);
    }
    
    #endregion
}