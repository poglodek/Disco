using Disco.Service.Discounts.Application.Exceptions;
using Disco.Service.Discounts.Core.Repositories;
using MediatR;

namespace Disco.Service.Discounts.Application.Commands.Handlers;

public class RemoveDiscountHandler : IRequestHandler<RemoveDiscount,Unit>
{
    private readonly IDiscountRepository _repository;

    public RemoveDiscountHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(RemoveDiscount request, CancellationToken cancellationToken)
    {
        var discount = await _repository.GetByIdAsync(request.Id);

        if (discount is null)
        {
            throw new DiscountNotFoundException(request.Id);
        }

        await _repository.DeleteAsync(discount);
        
        return Unit.Value;
    }
}