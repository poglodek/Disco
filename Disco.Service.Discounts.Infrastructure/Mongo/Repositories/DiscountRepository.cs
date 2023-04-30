using AutoMapper;
using Disco.Service.Discounts.Core.Entities;
using Disco.Service.Discounts.Core.Repositories;
using Disco.Service.Discounts.Infrastructure.Mongo.Documents;
using Disco.Shared.Mongo.Repository;

namespace Disco.Service.Discounts.Infrastructure.Mongo.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IMongoRepository<DiscountDocument, Guid> _mongoRepository;
    private readonly IMapper _mapper;

    public DiscountRepository(IMongoRepository<DiscountDocument,Guid> mongoRepository, IMapper mapper)
    {
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }
    
    public async Task<Discount> GetByIdAsync(Guid id)
    {
        var doc = await _mongoRepository.GetAsync(x => x.Id == id);
        if (doc is null)
        {
            return null;
        }
        
        return new Discount(doc.Id,doc.CompanyId,doc.Percent,doc.Points,doc.StartedDate,doc.EndingDate,doc.Name);
    }

    public async Task<IReadOnlyCollection<Discount>> GetAllAsync()
    {
        var docs = await _mongoRepository.FindAsync(x => true);
        return docs
            .Select(doc =>
                new Discount(doc.Id, doc.CompanyId, doc.Percent, doc.Points, doc.StartedDate, doc.EndingDate, doc.Name))
            .ToList();
    }

    public Task AddAsync(Discount discount)
    {
        var doc = _mapper.Map<DiscountDocument>(discount);
        return _mongoRepository.AddAsync(doc);
    }

    public Task UpdateAsync(Discount discount)
    {
        var doc = _mapper.Map<DiscountDocument>(discount);
        return _mongoRepository.UpdateAsync(doc);
    }

    public Task DeleteAsync(Guid id)
    {
        return _mongoRepository.RemoveAsync(x=>x.Id == id);
    }

    public Task DeleteAsync(Discount discount)
    {
        return _mongoRepository.RemoveAsync(x=>x.Id == discount.Id.Value);
    }
}