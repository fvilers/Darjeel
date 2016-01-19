using System;

namespace Darjeel.Messaging
{
    public interface IEvent : IMessage
    {
        Guid SourceId { get; set; }
    }
}