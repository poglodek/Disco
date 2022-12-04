using Disco.Service.Barcodes.Core.Entities;

namespace Disco.Service.Barcodes.Core.Repositories;

public interface IBarcodeRepository
{
    Task<Barcode> GetAsync(Guid id);
    Task<Barcode> GetByUserIdAsync(Guid id);
    Task<Barcode> GetByCodeAsync(long id);
}