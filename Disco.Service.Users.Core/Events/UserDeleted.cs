using Disco.Service.Users.Core.Entities;

namespace Disco.Service.Users.Core.Events;

public class UserDeleted : IDomainEvent
{
    public UserDeleted(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}