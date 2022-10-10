using Disco.Service.Users.Core.Entities;

namespace Disco.Service.Users.Core.Events;

public class UserDeleted : IDomainEvent
{
    public UserDeleted(User user)
    {
        User = user;
    }

    public User User { get; }
}