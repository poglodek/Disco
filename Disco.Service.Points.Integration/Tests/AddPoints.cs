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

public class AddPoints: IClassFixture<DiscoAppFactory<Program>>
{
    private Task<HttpResponseMessage> Act(Application.Commands.AddPoints model)
        => _client.PutAsync($"AddPoints", ContentHelper.GetContent(model));


    [Fact]
    public async Task AddPoint_ToPointsDontExist_ShouldReturn400()
    {
        var model = new Application.Commands.AddPoints(Guid.NewGuid(), 10);

        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeFalse();

        var content = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        content.ShouldBe("user_doesnt_have_points");

    }
    [Fact]
    public async Task AddPoint_ToPointsWithNullId_ShouldReturn400()
    {
        var model = new Application.Commands.AddPoints(Guid.Empty, 10);

        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeFalse();

        var content = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        content.ShouldBe("user_doesnt_have_points");

    }
    
    [Fact]
    public async Task AddPoint_ToPointsExist_ShouldReturn200()
    {
        var id = Guid.NewGuid();
        var model = new Application.Commands.AddPoints(id, 10);

        await _pointsFixture.AddPoints(id, 10, Guid.NewGuid());
        
        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeTrue();


    }
    
    [Fact]
    public async Task AddPoint_ToPointsExistButNegativePoints_ShouldReturn400()
    {
        var id = Guid.NewGuid();
        var model = new Application.Commands.AddPoints(id, 10);

        await _pointsFixture.AddPoints(id, -10, Guid.NewGuid());
        
        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeFalse();
        var content = await ContentHelper.ReturnObjectFromContent<string>(response);
        
        content.ShouldBe("invalid_points");

    }
    
    [Fact]
    public async Task AddPoint_ToPointsExistBut0Points_ShouldReturn400()
    {
        var id = Guid.NewGuid();
        var model = new Application.Commands.AddPoints(id, 10);

        await _pointsFixture.AddPoints(id, 0, Guid.NewGuid());
        
        var response = await Act(model);
        
        response.IsSuccessStatusCode.ShouldBeTrue();


    }




    #region Arrange
    
    private readonly HttpClient _client;
    private readonly PointsFixture _pointsFixture;
    
    public AddPoints(DiscoAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _pointsFixture = new PointsFixture(factory.MongoFixture);
    }
    
    #endregion
}