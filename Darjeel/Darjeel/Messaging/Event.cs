using System;

namespace Darjeel.Messaging
{
    public abstract class Event : IEvent
    {
        public Guid SourceId { get; }

        protected Event(Guid sourceId)
        {
            if (sourceId == Guid.Empty) throw new ArgumentNullException(nameof(sourceId));
            SourceId = sourceId;
        }
    }
}