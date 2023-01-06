using Disco.Service.Barcodes.Core.Events;
using Disco.Service.Barcodes.Core.Expcetions;
using Disco.Service.Barcodes.Core.ValueObjects;

namespace Disco.Service.Barcodes.Core.Entities;

public class Barcode: AggregateRoot
{
    public Code Code { get; private set; }
    public UserId UserId { get; private set; }

    public Barcode(Guid id, Guid userId,long? code)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidAggregateIdException(id);
        }

        if (userId == Guid.Empty)
        {
            throw new InvalidUserIdException(userId);
        }
        
        if(code is null || code.ToString().Length is < 9 or > 19)
        {
            throw new InvalidCodeLengthException();
        }

        if (code.Value <= 0)
        {
            throw new InvalidCodeLengthException();
        }
        
        Id = new AggregateId(id);
        Code = new Code(code.Value);
        UserId = new UserId(userId);
    }

    public static Barcode Create(Guid id, Guid userId, long? code = null)
    {
        if (code is null)
        {
            code = GenerateNewCode();
        }
        
        var barcode = new Barcode(id, userId, code);
        barcode.AddEvent(new BarcodeCreated(barcode.Id.Value));

        return barcode;
    }

    public void CreateNewCode()
    {
        Code = new Code(GenerateNewCode());
    }
    
    private static long GenerateNewCode()
    {
        var code = string.Join("", Guid.NewGuid().ToByteArray()).AsSpan(0,18);
        return long.Parse(code);
    }
}