using Disco.Service.Discounts.Core.Entities;
using Disco.Service.Discounts.Core.Repositories;
using MediatR;

namespace Disco.Service.Discounts.Application.Commands.Handlers;

public class AddDiscountHandler : IRequestHandler<AddDiscount,Unit>
{
    private readonly IDiscountRepository _repository;

    public AddDiscountHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(AddDiscount request, CancellationToken cancellationToken)
    {
        var discount = Discount.Create(Guid.NewGuid(), request.ComapnyId, request.Percent, request.Points, request.StatedDate,
            request.EndingDate, request.Name);

        await _repository.AddAsync(discount);
        
        return Unit.Value;
    }
}