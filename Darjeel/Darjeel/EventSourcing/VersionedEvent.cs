using Darjeel.Messaging;

namespace Darjeel.EventSourcing
{
    public class VersionedEvent : Event, IVersionedEvent
    {
        public int Version { get; set; }
    }
}
