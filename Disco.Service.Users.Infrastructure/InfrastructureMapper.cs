using AutoMapper;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Infrastructure.Mongo.Documents;

namespace Disco.Service.Users.Infrastructure;

public class InfrastructureMapper : Profile
{
    public InfrastructureMapper()
    {
        CreateMap<UserDocument, User>()
            .ConstructUsing(x =>
                new User(new AggregateId(x.Id), x.Email, x.Nick,x.Verified, x.CreatedDate, x.IsDeleted,x.Role));

        CreateMap<User, UserDocument>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate.Value))
            .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email.Value))
            .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role.Value))
            .ForMember(x => x.Verified, opt => opt.MapFrom(x => x.Verified.Value))
            .ForMember(x => x.IsDeleted, opt => opt.MapFrom(x => x.IsDeleted.Value))
            .ForMember(x => x.PasswordHash, opt => opt.MapFrom(x => x.PasswordHash.Value))
            .ForMember(x => x.Nick, opt => opt.MapFrom(x => x.Nick.Value));
    }
}