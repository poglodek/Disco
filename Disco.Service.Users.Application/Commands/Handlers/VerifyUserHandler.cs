using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Repositories;
using MediatR;

namespace Disco.Service.Users.Application.Commands.Handlers;

public class VerifyUserHandler : IRequestHandler<VerifyUser, Unit>
{
    private readonly IUserRepository _repository;

    public VerifyUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(VerifyUser request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAsync(request.Id);

        if (user is null)
        {
            throw new NotFoundUserException(request.Id);
        }

        user.VerifyUser();
        
        await _repository.UpdateAsync(user);
        
        return Unit.Value;
    }
}