using Darjeel.Infrastructure.Messaging;

namespace Darjeel.Infrastructure.EventSourcing
{
    public interface IVersionedEvent : IEvent
    {
        int Version { get; }
    }
}