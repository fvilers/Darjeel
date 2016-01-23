using Darjeel.Messaging;
using Darjeel.Messaging.Handling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Memory.Messaging
{
    public class InProcCommandBus : ICommandBus
    {
        private readonly ICommandHandlerRegistry _registry;

        public InProcCommandBus(ICommandHandlerRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }
            _registry = registry;
        }

        public async Task SendAsync(Envelope<ICommand> envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            var message = envelope.Body;
            var commandType = message.GetType();
            ICommandHandler handler;

            if (_registry.TryGetHandler(commandType, out handler))
            {
                Logging.DarjeelMemory.TraceInformation($"Command '{commandType.FullName}' handled by '{handler.GetType().FullName}.");
                await ((dynamic)handler).HandleAsync((dynamic)message);
            }
            else if (_registry.TryGetHandler(typeof(ICommand), out handler))
            {
                Logging.DarjeelMemory.TraceInformation($"Command '{commandType.FullName}' handled by '{handler.GetType().FullName}.");
                await ((dynamic)handler).HandleAsync((dynamic)message);
            }
        }

        public async Task SendAsync(IEnumerable<Envelope<ICommand>> envelopes)
        {
            if (envelopes == null) throw new ArgumentNullException(nameof(envelopes));

            foreach (var envelope in envelopes)
            {
                await SendAsync(envelope);
            }
        }
    }
}