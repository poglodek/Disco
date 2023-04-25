namespace Disco.Service.Discounts.Application.Services;

public interface IPointsService
{
    Task<bool> RemovePoints(Guid userId, int points);
}