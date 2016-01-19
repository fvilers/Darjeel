using System;

namespace Darjeel.Messaging
{
    public abstract class Event : IEvent
    {
        public Guid SourceId { get; set; }
    }
}