using System.Linq.Expressions;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Infrastructure.Mongo.Documents;

namespace Disco.Service.Users.Infrastructure.Mongo.Repositories;

public interface IUserRepository
{
    Task<User> GetAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsAsync(Expression<Func<UserDocument,bool>> expression);
    Task AddAsync(User resource);
    Task UpdateAsync(User resource);
    Task DeleteAsync(Guid id);
}