using Disco.Service.Discounts.Application.Dto;
using Disco.Service.Discounts.Core.Repositories;
using MediatR;

namespace Disco.Service.Discounts.Infrastructure.Query.QueryHandler;

public class GetDiscountsHandler : IRequestHandler<GetDiscounts,IReadOnlyCollection<DiscountDto>>
{
    private readonly IDiscountRepository _repository;

    public GetDiscountsHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IReadOnlyCollection<DiscountDto>> Handle(GetDiscounts request, CancellationToken cancellationToken)
    {
        var models = await _repository.GetAllAsync();
        
        return models
            .Select(x=>
                new DiscountDto(x.Id.Value,x.Company.Id,x.Percent.Value,x.Points.Value,x.StartedDate.Value,x.EndingDate.Value,x.Name.Value))
            .ToList();
    }
}