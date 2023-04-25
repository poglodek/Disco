using MediatR;

namespace Disco.Service.Discounts.Application.Commands.Handlers;

public class UseDiscountsHandler : IRequestHandler<UseDiscounts,Unit>
{
    public Task<Unit> Handle(UseDiscounts request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}