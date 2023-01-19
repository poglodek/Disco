using System;
using System.Linq;
using Disco.Service.Points.Core.Events;
using Disco.Service.Points.Core.Exceptions;
using Shouldly;
using Xunit;

namespace Disco.Service.Points.Unit.Domain;

public class PointsTest
{
    [Fact]
    public void CreatePoints_AllValid_ShouldReturnObject()
    {
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var points = 5;

        var obj = Core.Entities.Points.Create(id, userId, points);
        
        obj.Id.Value.ShouldBe(id);
        obj.UserId.Value.ShouldBe(userId);
        obj.PointValue.Value.ShouldBe(points);
    }
    
    [Fact]
    public void CreatePoints_InvalidUserId_ShouldReturnObject()
    {
        var id = Guid.NewGuid();
        var userId = Guid.Empty;
        var points = 5;

        var ex = Record.Exception(() => Core.Entities.Points.Create(id, userId, points));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidUserIdException>();
        
        ((InvalidUserIdException)ex).Code.ShouldBe("invalid_user_id");

    }
    
    [Fact]
    public void CreatePoints_InvalidId_ShouldReturnObject()
    {
        var id = Guid.Empty;
        var userId = Guid.NewGuid();
        var points = 5;

        var ex = Record.Exception(() => Core.Entities.Points.Create(id, userId, points));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidAggregateIdException>();
        
        ((InvalidAggregateIdException)ex).Code.ShouldBe("invalid_aggregate_id");

    }
    
    [Fact]
    public void CreatePoints_InvalidPointsValue_ShouldReturnObject()
    {
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var points = -12;

        var ex = Record.Exception(() => Core.Entities.Points.Create(id, userId, points));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidPointsValueException>();
        
        ((InvalidPointsValueException)ex).Code.ShouldBe("invalid_points");

    }

    [Fact]
    public void ClearPoints_With10Points_ShouldSetPointsTo0()
    {
        var obj = ReturnObject(Guid.NewGuid(), Guid.NewGuid(), 10);
        
        obj.ClearPoints();
        
        obj.PointValue.Value.ShouldBe(0);
        obj.Events.Count().ShouldBe(1);
        obj.Events.FirstOrDefault().ShouldBeOfType<PointsCleared>();

    }
    
    [Fact]
    public void ClearPoints_With0Points_ShouldSetPointsTo0()
    {
        var obj = ReturnObject(Guid.NewGuid(), Guid.NewGuid(), 0);
        
        obj.ClearPoints();
        
        obj.PointValue.Value.ShouldBe(0);
        obj.Events.Count().ShouldBe(1);
        obj.Events.FirstOrDefault().ShouldBeOfType<PointsCleared>();

    }
    
    [Fact]
    public void AddPoints_Add0To10_ShouldSetPointsTo10()
    {
        var obj = ReturnObject(Guid.NewGuid(), Guid.NewGuid(), 10);
        
        obj.AddPoints(0);
        
        obj.PointValue.Value.ShouldBe(10);
        obj.Events.Count().ShouldBe(1);
        obj.Events.FirstOrDefault().ShouldBeOfType<PointsAdded>();

    }
    
    [Fact]
    public void AddPoints_Add12To12_ShouldSetPointsTo24()
    {
        var obj = ReturnObject(Guid.NewGuid(), Guid.NewGuid(), 12);
        
        obj.AddPoints(12);
        
        obj.PointValue.Value.ShouldBe(24);
        obj.Events.Count().ShouldBe(1);
        obj.Events.FirstOrDefault().ShouldBeOfType<PointsAdded>();

    }
    
    [Fact]
    public void AddPoints_Add15ToNegative5_ShouldThrowAnEx()
    {
        var obj = ReturnObject(Guid.NewGuid(), Guid.NewGuid(), 12);
        
        var ex = Record.Exception(()=>obj.AddPoints(-5));
        
        ex.ShouldNotBeNull();

        ex.ShouldBeOfType<InvalidPointsOperationException>();

    }
    
    [Fact]
    public void SubtractPoints_15To5_ShouldSetPointsTo10()
    {
        var obj = ReturnObject(Guid.NewGuid(), Guid.NewGuid(), 15);

        obj.SubtractPoints(5);
        
        obj.PointValue.Value.ShouldBe(10);
        obj.Events.Count().ShouldBe(1);
        obj.Events.FirstOrDefault().ShouldBeOfType<PointsSubtracted>();
        

    }
    
    [Fact]
    public void SubtractPoints_12To0_ShouldSetPointsTo12()
    {
        var obj = ReturnObject(Guid.NewGuid(), Guid.NewGuid(), 12);

        obj.SubtractPoints(0);
        
        obj.PointValue.Value.ShouldBe(12);
        obj.Events.Count().ShouldBe(1);
        obj.Events.FirstOrDefault().ShouldBeOfType<PointsSubtracted>();
        

    }
    
    
    [Fact]
    public void SubtractPoints_22ToNegative8_ShouldThrowAnEx()
    {
        var obj = ReturnObject(Guid.NewGuid(), Guid.NewGuid(), 22);
        
        var ex = Record.Exception(()=>obj.SubtractPoints(-8));
        
        ex.ShouldNotBeNull();

        ex.ShouldBeOfType<InvalidPointsOperationException>();
        ((InvalidPointsOperationException)ex).Code.ShouldBe("wrong_operation");

    }
    
    [Fact]
    public void SubtractPoints_6To9_ShouldThrowAnEx()
    {
        var obj = ReturnObject(Guid.NewGuid(), Guid.NewGuid(), 6);
        
        var ex = Record.Exception(()=>obj.SubtractPoints(9));
        
        ex.ShouldNotBeNull();

        ex.ShouldBeOfType<NotEnoughPointsException>();
        ((NotEnoughPointsException)ex).Code.ShouldBe("points_are_not_enough");

    }
    


    private Core.Entities.Points ReturnObject(Guid id, Guid userId, int points = 0)
        => new Core.Entities.Points(id, userId, points);
}