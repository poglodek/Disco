using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Exceptions;
using Disco.Service.Users.Core.Repositories;
using MediatR;

namespace Disco.Service.Users.Application.Commands.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUser, Unit>
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAsync(request.Id);

        if (user is null)
        {
            throw new NotFoundUserException(request.Id);
        }

        if (user.IsDeleted)
        {
            throw new UserAlreadyDeletedException(request.Id);
        }
        
        user.SoftDeleteUser();

        await _repository.UpdateAsync(user);

        return Unit.Value;
    }
}