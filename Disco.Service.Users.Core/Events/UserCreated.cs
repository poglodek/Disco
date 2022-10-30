using Disco.Service.Users.Core.Entities;

namespace Disco.Service.Users.Core.Events;

public class UserCreated : IDomainEvent
{
    public UserCreated(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}