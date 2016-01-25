using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Messaging.Handling
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IEventHandlerRegistry _registry;

        public EventDispatcher(IEventHandlerRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));
            _registry = registry;
        }

        public async Task DispatchEventAsync(IEvent @event, string correlationId = null)
        {
            var eventType = @event.GetType();
            IEnumerable<IEventHandler> handlers;
            var tasks = new List<Task>();

            if (_registry.TryGetHandlers(eventType, out handlers))
            {
                foreach (var handler in handlers)
                {
                    Logging.Darjeel.TraceInformation($"Event '{eventType.FullName}' handled by '{handler.GetType().FullName}.");
                    var task = ((dynamic)handler).HandleAsync((dynamic)@event);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}
