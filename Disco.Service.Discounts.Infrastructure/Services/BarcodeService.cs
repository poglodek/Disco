using Disco.Service.Discounts.Application.Dto;
using Disco.Service.Discounts.Application.Services;
using Disco.Service.Discounts.Infrastructure.Exceptions;
using Disco.Shared.Fabio.HttpClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Disco.Service.Discounts.Infrastructure.Services;

public class BarcodeService : IBarcodeService
{
    private readonly IFabioHttpClient _httpClient;
    private readonly ILogger<BarcodeService> _logger;

    public BarcodeService(IFabioHttpClient httpClient, ILogger<BarcodeService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<UserIdDto> GetUserByBarCode(long barcode)
    {
        try
        {
            var response = await _httpClient.GetAsync($"GetUserIdByBarcode/{barcode}");

            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync();
            return  JsonConvert.DeserializeObject<UserIdDto>(await json);

        }
        catch (Exception e)
        {
           _logger.LogError(e.Message,e);

           throw new UserNotFoundException(barcode);
        }
    }
}