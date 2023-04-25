using Disco.Service.Discounts.Application.Dto;

namespace Disco.Service.Discounts.Application.Services;

public interface IBarcodeService
{
    Task<UserIdDto> GetUserByBarCode(long barcode);
}