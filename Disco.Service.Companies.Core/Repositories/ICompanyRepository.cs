using Disco.Service.Core.Entities;

namespace Disco.Service.Core.Repositories;

public interface ICompanyRepository
{
    public Task AddAsync(Company company);
    public Task<Company> GetAsync(Guid id);
}