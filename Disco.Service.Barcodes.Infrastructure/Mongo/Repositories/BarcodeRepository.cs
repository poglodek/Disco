using AutoMapper;
using Disco.Service.Barcodes.Core.Entities;
using Disco.Service.Barcodes.Core.Repositories;
using Disco.Service.Barcodes.Infrastructure.Mongo.Documents;
using Disco.Shared.Mongo.Repository;

namespace Disco.Service.Barcodes.Infrastructure.Mongo.Repositories;

internal sealed class BarcodeRepository : IBarcodeRepository
{
    private readonly IMongoRepository<BarcodeDocument, Guid> _repository;
    private readonly IMapper _mapper;

    public BarcodeRepository(IMongoRepository<BarcodeDocument,Guid> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Barcode> GetAsync(Guid id)
    {
        var document = await _repository.GetAsync(x=>x.Id.Equals(id));
        return _mapper.Map<Barcode>(document);
    }

    public async Task<Barcode> GetByUserIdAsync(Guid id)
    {
        var document = await _repository.GetAsync(x=>x.UserId.Equals(id));
        return _mapper.Map<Barcode>(document);
    }

    public async Task<Barcode> GetByCodeAsync(long id)
    {
        var document = await _repository.GetAsync(x=>x.Code.Equals(id));
        return _mapper.Map<Barcode>(document);
    }

    public Task SaveBarCode(Barcode barcode)
    {
        var document = _mapper.Map<BarcodeDocument>(barcode);
        return _repository.AddAsync(document);
    }
}