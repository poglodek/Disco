using AutoMapper;
using Disco.Service.Points.Core.Repositories;
using Disco.Service.Points.Infrastructure.Mongo.Documents;
using Disco.Shared.Mongo.Repository;

namespace Disco.Service.Points.Infrastructure.Mongo.Repositories;

internal class PointsRepository : IPointsRepository
{
    private readonly IMongoRepository<PointsDocument, Guid> _repository;
    private readonly IMapper _mapper;

    public PointsRepository(IMongoRepository<PointsDocument,Guid> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<Core.Entities.Points> GetByUserIdAsync(Guid id)
    {
        var obj = await _repository.GetAsync(x => x.UserId.Equals(id));
        return _mapper.Map<Core.Entities.Points>(obj);
    }

    public Task UpdateAsync(Core.Entities.Points resource)
    {
        var obj = _mapper.Map<PointsDocument>(resource);
        return _repository.UpdateAsync(obj);
    }

    public Task AddAsync(Core.Entities.Points resource)
    {
        var obj = _mapper.Map<PointsDocument>(resource);
        return _repository.AddAsync(obj);
    }

    public async Task<Core.Entities.Points> GetByIdAsync(Guid id)
    {
        var obj = await _repository.GetAsync(x=>x.Id == id);

        return _mapper.Map<Core.Entities.Points>(obj);
    }
}