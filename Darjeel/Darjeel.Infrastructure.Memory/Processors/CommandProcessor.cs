using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Messaging.Handling;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

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

        protected override async Task ProcessMessageAsync(ICommand message, string correlationId)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

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
    }
}