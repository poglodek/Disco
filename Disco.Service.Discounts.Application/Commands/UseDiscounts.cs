using MediatR;

namespace Disco.Service.Discounts.Application.Commands;

public class UseDiscounts : IRequest<Unit>
{
    public string Barcode { get; set; }
    public Guid Id { get; set; }
}