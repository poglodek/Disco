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
                new User(new AggregateId(x.Id), x.Email, x.Nick,x.Verified, x.CreatedDate, x.IsDeleted));

        CreateMap<User, UserDocument>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Value));
    }
}