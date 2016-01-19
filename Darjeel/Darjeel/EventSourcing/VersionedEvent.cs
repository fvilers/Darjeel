using Darjeel.Messaging;

namespace Darjeel.EventSourcing
{
    public class VersionedEvent : Event, IVersionedEvent
    {
        public int Version { get; set; }
        public IEvent Event { get; set; }
    }
}
