using System;
using System.Linq;
using Disco.Service.Barcodes.Core.Entities;
using Disco.Service.Barcodes.Core.Expcetions;
using Shouldly;
using Xunit;

namespace Disco.Service.Barcodes.Unit.Domain;

public class BarcodeTest
{
    private Barcode Act(Guid id, long code, Guid userId)
        => new Barcode(id, userId, code);

    [Fact]
    public void CreateValid_ShouldReturnBarcode()
    {
        var id = Guid.NewGuid();
        var code = ReturnValidCode();
        var userId = Guid.NewGuid();

        var barcode = Barcode.Create(id, userId, code);
        
        barcode.Code.Value.ShouldBe(code);
        barcode.Id.Value.ShouldBe(id);
        barcode.UserId.Value.ShouldBe(userId);
        
        barcode.Events.Count().ShouldBe(1);
    }

    [Fact]
    public void SetNewCode_ShouldPass()
    {
        var id = Guid.NewGuid();
        var code = ReturnValidCode();
        var userId = Guid.NewGuid();

        var barcode = Act(id, code, userId);
        var oldCode = barcode.Code.Value;
        
        barcode.CreateNewCode();
        
        barcode.Code.Value.ShouldNotBe(oldCode);
        oldCode.ShouldBe(code);
    }
    
    [Fact]
    public void ClearingEvents_ShouldPass()
    {
        var id = Guid.NewGuid();
        var code = ReturnValidCode();
        var userId = Guid.NewGuid();

        var barcode = Act(id, code, userId);
        
        barcode.ClearEvents();
        barcode.Events.Count().ShouldBe(0);
        
    }
    
    [Fact]
    public void CreatingBarCodeWithInvalidUserId_ShouldThrowAnException()
    {
        var id = Guid.NewGuid();
        var code = ReturnValidCode();
        var userId = Guid.Empty;

        var ex = Record.Exception(() => Barcode.Create(id, userId, code));

        ex.ShouldNotBeNull();

        ex.ShouldBeOfType<InvalidUserIdException>();
        ((DomainException)ex).Code.ShouldBe("invalid_user_id");

    }

    [Fact]
    public void CreatingBarCodeWithInvalidId_ShouldThrowAnException()
    {
        var userId = Guid.NewGuid();
        var code = ReturnValidCode();
        var id = Guid.Empty;

        var ex = Record.Exception(() => Barcode.Create(id, userId, code));

        ex.ShouldNotBeNull();

        ex.ShouldBeOfType<InvalidAggregateIdException>();
        ((DomainException)ex).Code.ShouldBe("invalid_aggregate_id");

    }
    [Fact]
    public void CreatingBarCodeWithMinLongAsCode_ShouldThrowAnException()
    {
        var userId = Guid.NewGuid();
        var code = long.MinValue;
        var id = Guid.NewGuid();

        var ex = Record.Exception(() => Barcode.Create(id, userId, code));

        ex.ShouldNotBeNull();

        ex.ShouldBeOfType<InvalidCodeLengthException>();
        ((DomainException)ex).Code.ShouldBe("invalid_code_lenght");

    }
    [Fact]
    public void CreatingBarCodeWithMaxLongAsCode_ShouldPass()
    {
        var userId = Guid.NewGuid();
        var code = long.MaxValue;
        var id = Guid.NewGuid();

        var ex = Record.Exception(() => Barcode.Create(id, userId, code));

        ex.ShouldBeNull();
        
    }


    private long ReturnValidCode()
    {
        var code = string.Join("", Guid.NewGuid().ToByteArray()).Substring(0,18);
        return long.Parse(code);
    }
}