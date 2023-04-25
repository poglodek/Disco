using Disco.Service.Discounts.Application.Dto;
using Disco.Service.Discounts.Core.Entities;
using MediatR;

namespace Disco.Service.Discounts.Infrastructure.Query;

public class GetDiscounts : IRequest<IReadOnlyCollection<DiscountDto>>
{
    
}