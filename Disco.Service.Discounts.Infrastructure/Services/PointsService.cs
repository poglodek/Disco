using Disco.Service.Discounts.Application.Services;
using Disco.Service.Discounts.Infrastructure.Exceptions;
using Disco.Shared.Fabio.HttpClient;
using Microsoft.Extensions.Logging;

namespace Disco.Service.Discounts.Infrastructure.Services;

public class PointsService : IPointsService
{
    private readonly IFabioHttpClient _httpClient;
    private readonly ILogger<PointsService> _logger;

    public PointsService(IFabioHttpClient httpClient, ILogger<PointsService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<bool> RemovePoints(Guid userId, int points)
    {
        try
        {
            var response = await _httpClient.PutAsync("disco-points/SubtractPoints", new {PointsId = userId, Points = points });

            response.EnsureSuccessStatusCode();
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message,e);

            throw new PointsNotFoundException(userId);
        }
        
    }
}