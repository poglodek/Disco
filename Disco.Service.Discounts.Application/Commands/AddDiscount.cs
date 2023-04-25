using MediatR;

namespace Disco.Service.Discounts.Application.Commands;

public class AddDiscount : IRequest<Unit>
{
    public Guid ComapnyId { get; }
    public int Percent { get; }
    public int Points { get; }
    public string Name { get; }
    public DateOnly StatedDate { get; }
    public DateOnly EndingDate { get; }
}