using System.Collections.Generic;

namespace Darjeel.EventSourcing
{
    public interface IEventSourced
    {
        int Version { get; }
        IEnumerable<IVersionedEvent> UncommittedEvents { get; }
    }
}