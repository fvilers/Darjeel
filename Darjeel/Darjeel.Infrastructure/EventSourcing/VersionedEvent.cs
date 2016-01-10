using Darjeel.Infrastructure.Messaging;
using System;

namespace Darjeel.Infrastructure.EventSourcing
{
    public abstract class VersionedEvent : Event, IVersionedEvent
    {
        public int Version { get; }

        protected VersionedEvent(Guid sourceId, int version)
            : base(sourceId)
        {
            if (version < 0) throw new ArgumentOutOfRangeException(nameof(version));
            Version = version;
        }
    }
}
