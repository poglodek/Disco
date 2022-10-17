using AutoMapper;
using Disco.Service.Users.Application.Dto;
using Disco.Service.Users.Application.Queries;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Repositories;
using MediatR;

namespace Disco.Service.Users.Infrastructure.QueryHandlers;

public class GetUserInformationHandler : IRequestHandler<GetUserInformation,UserDto>
{
    private readonly IUserRepository _repository;

    public GetUserInformationHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<UserDto> Handle(GetUserInformation request, CancellationToken cancellationToken)
    {
        if (request.Id.Equals(Guid.Empty))
        {
            return null;
        }

        var user = await _repository.GetAsync(request.Id);

        if (user is null || user.IsDeleted)
        {
            return null;
        }
        
        return new UserDto(user.Id.Value, user.Email, user.Nick,user.Verified, user.CreatedDate);


    }
}