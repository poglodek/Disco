using Disco.Service.Discounts.Core.Entities;

namespace Disco.Service.Discounts.Core.Repositories;

public interface IDiscountRepository
{
    Task<Discount> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<Discount>> GetAllAsync();
    Task AddAsync(Discount discount);
    Task UpdateAsync(Discount discount);
    Task DeleteAsync(Guid id);
    Task DeleteAsync(Discount discount);
}