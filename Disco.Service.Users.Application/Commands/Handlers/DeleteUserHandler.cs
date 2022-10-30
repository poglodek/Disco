using Disco.Service.Users.Application.Exceptions;
using Disco.Service.Users.Core.Exceptions;
using Disco.Service.Users.Core.Repositories;
using Disco.Shared.Rabbit.OutboxPattern.Services;
using MediatR;

namespace Disco.Service.Users.Application.Commands.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUser, Unit>
{
    private readonly IUserRepository _repository;
    private readonly IEventProcessor _eventProcessor;

    public DeleteUserHandler(IUserRepository repository, IEventProcessor eventProcessor)
    {
        _repository = repository;
        _eventProcessor = eventProcessor;
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

        await _eventProcessor.ProcessAsync(user.Events);
        
        return Unit.Value;
    }
}