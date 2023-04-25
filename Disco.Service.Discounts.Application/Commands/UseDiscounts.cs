using Disco.Service.Discounts.Application.Responses;
using MediatR;

namespace Disco.Service.Discounts.Application.Commands;

public class UseDiscounts : IRequest<UseDiscountResponse>
{
    public UseDiscounts(long barcode, Guid id)
    {
        Barcode = barcode;
        Id = id;
    }

    public long Barcode { get; }
    public Guid Id { get; }
}