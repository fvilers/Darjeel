using System.Collections.Generic;

namespace Darjeel.Infrastructure.EventSourcing
{
    public interface IEventSourced
    {
        int Version { get; }
        IEnumerable<IVersionedEvent> UncommittedEvents { get; }
    }
}