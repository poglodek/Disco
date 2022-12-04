namespace Disco.Service.Barcodes.Core.Expcetions;

public class InvalidCodeLengthException : DomainException
{
    public InvalidCodeLengthException() : base($"Length must be between 10 and 18")
    {
    }

    public override string Code => "invalid_code_lenght";
}