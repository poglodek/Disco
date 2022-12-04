using AutoMapper;
using Disco.Service.Barcodes.Core.Entities;
using Disco.Service.Barcodes.Infrastructure.Mongo.Documents;

namespace Disco.Service.Barcodes.Infrastructure;

public class InfrastructureMapper : Profile
{
    public InfrastructureMapper()
    {
        CreateMap<BarcodeDocument, Barcode>()
            .ConstructUsing(x =>
                new Barcode(x.Id,x.UserId,x.Code));

        CreateMap<Barcode, BarcodeDocument>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x=>x.Code,opt => opt.MapFrom(c=>c.Code.Value))
            .ForMember(x=>x.UserId,opt => opt.MapFrom(c=>c.UserId.Value));
    }
}