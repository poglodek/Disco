using AutoMapper;
using Disco.Service.Discounts.Core.Entities;
using Disco.Service.Discounts.Infrastructure.Mongo.Documents;

namespace Disco.Service.Discounts.Infrastructure.Mappers;

public class DiscountMapper : Profile
{
    public DiscountMapper()
    {
        CreateMap<Discount, DiscountDocument>()
            .ForMember(x=>x.Id,z=>z.MapFrom(c=>c.Id.Value))
            .ForMember(x=>x.Points,z=>z.MapFrom(c=>c.Points.Value))
            .ForMember(x=>x.Percent,z=>z.MapFrom(c=>c.Percent.Value))
            .ForMember(x=>x.CompanyId,z=>z.MapFrom(c=>c.Company.Id))
            .ForMember(x=>x.Name,z=>z.MapFrom(c=>c.Name.Value))
            .ForMember(x=>x.StartedDate,z=>z.MapFrom(c=>c.StartedDate.Value))
            .ForMember(x=>x.EndingDate,z=>z.MapFrom(c=>c.EndingDate.Value));
        
    }
}