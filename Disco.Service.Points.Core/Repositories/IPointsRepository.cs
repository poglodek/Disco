namespace Disco.Service.Points.Core.Repositories;

public interface IPointsRepository
{
    Task<Points.Core.Entities.Points> GetByUserIdAsync(Guid id);
    Task UpdateAsync(Points.Core.Entities.Points resource);
    Task AddAsync(Points.Core.Entities.Points resource);
}