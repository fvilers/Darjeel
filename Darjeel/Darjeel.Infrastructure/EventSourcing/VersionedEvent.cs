using Darjeel.Infrastructure.Messaging;
using System;

namespace Darjeel.Infrastructure.EventSourcing
{
    public class VersionedEvent : Event, IVersionedEvent
    {
        public int Version { get; }
        public IEvent Event { get; }

        public VersionedEvent(Guid sourceId, int version, IEvent @event)
            : base(sourceId)
        {
            if (version < 0) throw new ArgumentOutOfRangeException(nameof(version));
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            Version = version;
            Event = @event;
        }
    }
}
