using System.Linq.Expressions;
using Disco.Service.Users.Core.Entities;

namespace Disco.Service.Users.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(User resource);
    Task UpdateAsync(User resource);
    Task DeleteAsync(Guid id);
}