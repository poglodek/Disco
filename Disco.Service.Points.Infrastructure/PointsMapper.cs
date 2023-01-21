using AutoMapper;
using Disco.Service.Points.Application.Dto;
using Disco.Service.Points.Infrastructure.Mongo.Documents;

namespace Disco.Service.Points.Infrastructure;

public class PointsMapper : Profile
{
    public PointsMapper()
    {
        CreateMap<Core.Entities.Points, PointsDocument>()
            .ForMember(x => x.Points, z => z.MapFrom(c => c.PointValue.Value))
            .ForMember(x => x.UserId, z => z.MapFrom(c => c.UserId.Value))
            .ForMember(x => x.Id, z => z.MapFrom(c => c.Id.Value))
            .ReverseMap();
        
        CreateMap<Core.Entities.Points, PointsDto>()
            .ForMember(x => x.Points, z => z.MapFrom(c => c.PointValue.Value))
            .ForMember(x => x.UserId, z => z.MapFrom(c => c.UserId.Value))
            .ForMember(x => x.Id, z => z.MapFrom(c => c.Id.Value))
            .ReverseMap();
        
    }
}