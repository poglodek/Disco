using System;
using Disco.Service.Barcodes.Core.Entities;
using Disco.Service.Barcodes.Core.Expcetions;
using Shouldly;
using Xunit;

namespace Disco.Service.Barcodes.Unit.Domain;

public class AggreateTest
{
    [Fact]
    public void AggreateId_WithValidGuid()
    {
        var guid = Guid.NewGuid();
        var id = new AggregateId(guid);

        id.Value.ShouldNotBe(Guid.Empty);
        id.Value.ShouldBe(guid);
    }
    [Fact]
    public void AggreateId_WithEmptyGuid_ShouldThrowAnException()
    {
        var ex =  Record.Exception(() => new AggregateId(Guid.Empty));

        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidAggregateIdException>();
        ((InvalidAggregateIdException)ex).Code.ShouldBe("invalid_aggregate_id");
    }

    [Fact]
    public void AggregateId_Equals_ThisSameAggregateIdValue_ReturnTrue()
    {
        var guid = Guid.NewGuid();

        var id = new AggregateId(guid);
        var id2 = new AggregateId(guid);
        
        id.Equals(id2).ShouldBeTrue();
    }
    [Fact]
    public void AggregateId_Equals_OtherAggregateIdValue_ReturnFalse()
    {
        var id = new AggregateId(Guid.NewGuid());
        var id2 = new AggregateId(Guid.NewGuid());
        
        id.Equals(id2).ShouldBeFalse();
    }
    [Fact]
    public void AggregateId_Equals_Null_ReturnFalse()
    {
        var id = new AggregateId(Guid.NewGuid());
        
        id.Equals(null).ShouldBeFalse();
    }
    [Fact]
    public void ValidAggregateId_Return_Hashcode()
    {
        var id = new AggregateId(Guid.NewGuid());
        
        id.GetHashCode().ShouldNotBe(0);
    }
    [Fact]
    public void AggregateId_Equals_AsObjectThisSameAggregateIdValue_ReturnTrue()
    {
        var guid = Guid.NewGuid();

        var id = new AggregateId(guid);
        var id2 = new AggregateId(guid);
        
        id.Equals((object)id2).ShouldBeTrue();
    }
    [Fact]
    public void AggregateId_Equals_AsObjectOtherAggregateIdValue_ReturnFalse()
    {
        var id = new AggregateId(Guid.NewGuid());
        var id2 = new AggregateId(Guid.NewGuid());
        
        id.Equals((object)id2).ShouldBeFalse();
    }
    [Fact]
    public void AggregateId_Equals_AsObjectNullValue_ReturnFalse()
    {
        var id = new AggregateId(Guid.NewGuid());

        id.Equals((object)null).ShouldBeFalse();
    }
    [Fact]
    public void AggregateId_Equals_TheSameAggregateId_ReturnTrue()
    {
        var id = new AggregateId(Guid.NewGuid());

        id.Equals(id).ShouldBeTrue();
    }
    [Fact]
    public void AggregateId_Equals_OtherObject_ReturnFalse()
    {
        var id = new AggregateId(Guid.NewGuid());
        
        id.Equals("test").ShouldBeFalse();
    }
    [Fact]
    public void AggregateId_Equals_EmptyGuid_ReturnFalse()
    {
        var id = new AggregateId(Guid.NewGuid());
        
        id.Equals(Guid.Empty).ShouldBeFalse();
    }
    [Fact]
    public void AggregateId_Equals_NewGuid_ReturnFalse()
    {
        var id = new AggregateId(Guid.NewGuid());
        
        id.Equals(Guid.NewGuid()).ShouldBeFalse();
    }
    [Fact]
    public void AggregateId_Equals_NeObject_ReturnFalse()
    {
        var id = new AggregateId(Guid.NewGuid());
        
        id.Equals(new object()).ShouldBeFalse();
    }

}