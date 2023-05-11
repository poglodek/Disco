using AutoMapper;
using Disco.Service.Companies.Infrastructure.Mongo.Documents;
using Disco.Service.Core.Entities;
using Disco.Service.Core.Repositories;
using Disco.Shared.Mongo.Repository;

namespace Disco.Service.Companies.Infrastructure.Mongo.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly IMongoRepository<CompanyDocument, Guid> _repository;
    private readonly IMapper _mapper;

    public CompanyRepository(IMongoRepository<CompanyDocument,Guid> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task AddAsync(Company company)
    {
        var obj = _mapper.Map<CompanyDocument>(company);
        return _repository.AddAsync(obj);
    }

    public async Task<Company> GetAsync(Guid id)
    {
        var entity = await _repository.GetAsync(x=>x.Id.Equals(id));
        return _mapper.Map<Company>(entity);
    }
}