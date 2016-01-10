using System;

namespace Darjeel.Infrastructure.Messaging
{
    public interface IEvent : IMessage
    {
        Guid SourceId { get; }
    }
}