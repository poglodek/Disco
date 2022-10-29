using Disco.Service.Users.Core.Events;

namespace Disco.Service.Users.Application.Services;

public interface IEventProcessor
{
    Task ProcessAsync(IEnumerable<IDomainEvent> @events);
}