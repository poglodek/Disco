using System;
using Disco.Service.Discounts.Core.Entities;
using Disco.Service.Discounts.Core.Exceptions;
using Shouldly;
using Xunit;

namespace Disco.Service.Discounts.Unit.Core;

public class DiscountTest
{
    private DateOnly Now => DateOnly.FromDateTime(DateTime.Now);
    
    [Fact]
    public void CreateDiscount_InvalidId_ThrowException()
    {

        var ex =  Record.Exception(() => Discount.Create(Guid.Empty, Guid.NewGuid(), 5, 10, Now, Now,"Super name"));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidAggregateIdException>();


    }
    
    [Fact]
    public void CreateDiscount_InvalidCompanyId_ThrowException()
    {

        var ex =  Record.Exception(() => Discount.Create(Guid.NewGuid(), Guid.Empty, 5, 10, Now, Now,"Super name"));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidCompanyIdException>();


    }
    
    [Fact]
    public void CreateDiscount_PercentBelow0_ThrowException()
    {

        var ex =  Record.Exception(() => Discount.Create(Guid.NewGuid(), Guid.NewGuid(), -5, 10, Now, Now,"Super name"));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidPercentValueException>();


    }
    
    [Fact]
    public void CreateDiscount_PercentAbove100_ThrowException()
    {

        var ex =  Record.Exception(() => Discount.Create(Guid.NewGuid(), Guid.NewGuid(), 101, 10, Now, Now,"Super name"));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidPercentValueException>();


    }
    
    [Fact]
    public void CreateDiscount_PointsBelow0_ThrowException()
    {

        var ex =  Record.Exception(() => Discount.Create(Guid.NewGuid(), Guid.NewGuid(), 10, -10, Now, Now,"Super name"));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidPointsValueException>();


    }
    
    [Fact]
    public void CreateDiscount_InvalidDate_ThrowException()
    {

        var ex =  Record.Exception(() => Discount.Create(Guid.NewGuid(), Guid.NewGuid(), 10, 10, Now.AddDays(10), Now,"Super name"));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidDatesException>();


    }
    
    
    [Fact]
    public void CreateDiscount_InvalidName_ThrowException()
    {

        var ex =  Record.Exception(() => Discount.Create(Guid.NewGuid(), Guid.NewGuid(), 10, 10, Now, Now,"  "));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidNameException>();


    }
    
    [Fact]
    public void CreateDiscount_NullName_ThrowException()
    {

        var ex =  Record.Exception(() => Discount.Create(Guid.NewGuid(), Guid.NewGuid(), 10, 10, Now, Now,null));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidNameException>();


    }
    
    
    [Fact]
    public void CreateDiscount_CorrectValues_ReturnObject()
    {

        var obj = ReturnModel();

        obj.ShouldNotBeNull();

    }
    
    [Fact]
    public void SetPoints_CorrectValues_Ok()
    {

        var obj = ReturnModel();

        var ex = Record.Exception(() => obj.SetPoints(69));

        ex.ShouldBeNull();
        obj.Points.Value.ShouldBe(69);

    }
    
    [Fact]
    public void SetPoints_InCorrectValues_ThrowException()
    {

        var obj = ReturnModel();

        var ex = Record.Exception(() => obj.SetPoints(-105));

        ex.ShouldBeOfType<InvalidPointsValueException>();

    }
    
    [Fact]
    public void SetPercent_CorrectValues_Ok()
    {

        var obj = ReturnModel();

        var ex = Record.Exception(() => obj.SetPercent(69));

        ex.ShouldBeNull();
        obj.Percent.Value.ShouldBe(69);

    }
    
    [Fact]
    public void SetPercent_InCorrectValues_ThrowException()
    {

        var obj = ReturnModel();

        var ex = Record.Exception(() => obj.SetPercent(-105));

        ex.ShouldBeOfType<InvalidPercentValueException>();

    }
    
    [Fact]
    public void SetStartedDate_CorrectValues_Ok()
    {

        var obj = ReturnModel();

        var ex = Record.Exception(() => obj.SetStartedDate(Now.AddDays(-10)));

        ex.ShouldBeNull();
        obj.StartedDate.Value.ShouldBe(Now.AddDays(-10));

    }
    
    [Fact]
    public void SetStartedDate_InCorrectValues_ThrowException()
    {

        var obj = ReturnModel();

        var ex = Record.Exception(() => obj.SetStartedDate(Now.AddDays(10)));

        ex.ShouldBeOfType<InvalidDatesException>();

    }
    
    [Fact]
    public void SetEndingDateDate_CorrectValues_Ok()
    {

        var obj = ReturnModel();

        var ex = Record.Exception(() => obj.SetEndingDate(Now.AddDays(10)));

        ex.ShouldBeNull();
        obj.EndingDate.Value.ShouldBe(Now.AddDays(10));

    }
    
    [Fact]
    public void SetEndingDateDate_InCorrectValues_ThrowException()
    {

        var obj = ReturnModel();

        var ex = Record.Exception(() => obj.SetEndingDate(Now.AddDays(-10)));

        ex.ShouldBeOfType<InvalidDatesException>();

    }
    

    private Discount ReturnModel() => Discount.Create(Guid.NewGuid(), Guid.NewGuid(), 10, 10, Now, Now,"Super fancy name!");
}