using Disco.Service.Users.Core.Entities;

namespace Disco.Service.Users.Core.Events;

public class UserCreated : IDomainEvent
{
    public UserCreated(User user)
    {
        User = user;
    }

    public User User { get; }
}