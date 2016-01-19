using Darjeel.Messaging;

namespace Darjeel.EventSourcing
{
    public interface IVersionedEvent : IEvent
    {
        int Version { get; set; }
        IEvent Event { get; set; }
    }
}