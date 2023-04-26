using MediatR;

namespace Disco.Service.Discounts.Application.Commands;

public class RemoveDiscount : IRequest<Unit>
{
    public RemoveDiscount(Guid id)
    {
        Id = id;
    }

    public Guid Id { get;  }
}