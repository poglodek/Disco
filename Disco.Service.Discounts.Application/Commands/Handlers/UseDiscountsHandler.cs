using Disco.Service.Discounts.Application.Exceptions;
using Disco.Service.Discounts.Application.Responses;
using Disco.Service.Discounts.Application.Services;
using Disco.Service.Discounts.Core.Repositories;
using MediatR;

namespace Disco.Service.Discounts.Application.Commands.Handlers;

public class UseDiscountsHandler : IRequestHandler<UseDiscounts,UseDiscountResponse>
{
    private readonly IDiscountRepository _repository;
    private readonly IBarcodeService _barcodeService;
    private readonly IPointsService _pointsService;

    public UseDiscountsHandler(IDiscountRepository repository, IBarcodeService barcodeService, IPointsService pointsService)
    {
        _repository = repository;
        _barcodeService = barcodeService;
        _pointsService = pointsService;
    }
    
    public async Task<UseDiscountResponse> Handle(UseDiscounts request, CancellationToken cancellationToken)
    {
        var discount = await _repository.GetByIdAsync(request.Id);
        
        if (discount is null)
        {
            throw new DiscountNotFoundException(request.Id);
        }

        var user = await _barcodeService.GetUserByBarCode(request.Barcode);
        
        await _pointsService.RemovePoints(user.Id, discount.Points.Value);
        
        return new UseDiscountResponse(user.Id,discount.Percent.Value);
    }
}