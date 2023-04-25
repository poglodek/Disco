using MediatR;

namespace Disco.Service.Discounts.Application.Commands.Handlers;

public class AddDiscountHandler : IRequestHandler<AddDiscount,Unit>
{
    public Task<Unit> Handle(AddDiscount request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}