namespace Disco.Service.Barcodes.Application.Exception;

public class BarcodeNotFoundException : ApplicationException
{
    public BarcodeNotFoundException(Guid msg) : base($"Barcode for user with code:{msg.ToString()} wasn't found!")
    {
    }

    public override string Code => "barcode_not_found";
}