using Disco.Service.Users.Core.Entities;

namespace Disco.Service.Users.Core.Events;

public class UserVerified : IDomainEvent
{
    public UserVerified(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}