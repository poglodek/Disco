using Disco.Service.Users.Core.Entities;

namespace Disco.Service.Users.Core.Events;

public class UserVerified : IDomainEvent
{
    public UserVerified(User user)
    {
        User = user;
    }

    public User User { get; }
}