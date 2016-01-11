using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Messaging.Handling;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Darjeel.Infrastructure.Memory.Processors
{
    public class CommandProcessor : MessageProcessor<ICommand>
    {
        private readonly ICommandHandlerRegistry _registry;

        public CommandProcessor(ICommandHandlerRegistry registry, IProducerConsumerCollection<Envelope<ICommand>> commandCollection)
            : base(commandCollection)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));
            _registry = registry;
        }

        protected override void ProcessMessage(ICommand message, string correlationId)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var commandType = message.GetType();
            ICommandHandler handler;

            if (_registry.TryGetHandler(commandType, out handler))
            {
                Trace.TraceInformation("Command '{0}' handled by '{1}.", commandType.FullName, handler.GetType().FullName);
                ((dynamic)handler).Handle((dynamic)message);
            }
            else if (_registry.TryGetHandler(typeof(ICommand), out handler))
            {
                Trace.TraceInformation("Command '{0}' handled by '{1}.", commandType.FullName, handler.GetType().FullName);
                ((dynamic)handler).Handle((dynamic)message);
            }
        }
    }
}