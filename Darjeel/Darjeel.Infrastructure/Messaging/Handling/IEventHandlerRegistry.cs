using System;
using System.Collections.Generic;

namespace Darjeel.Infrastructure.Messaging.Handling
{
    public interface IEventHandlerRegistry : IRegistry<IEventHandler>
    {
        bool TryGetHandlers(Type eventType, out IEnumerable<IEventHandler> handlers);
    }
}