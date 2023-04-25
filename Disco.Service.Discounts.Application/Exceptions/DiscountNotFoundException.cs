namespace Disco.Service.Discounts.Application.Exceptions;

public class DiscountNotFoundException : AppException
{
    public DiscountNotFoundException(Guid msg) : base($"Discount with id:{msg} not found!")
    {
    }

    public override string Code => "discount_not_found";
}