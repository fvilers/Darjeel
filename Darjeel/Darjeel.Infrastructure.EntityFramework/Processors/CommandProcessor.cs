using Darjeel.Infrastructure.EntityFramework.Messaging;
using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Messaging.Handling;
using Darjeel.Infrastructure.Serialization;
using System;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.EntityFramework.Processors
{
    public class CommandProcessor : MessageProcessor<CommandEntity>
    {
        private readonly ICommandHandlerRegistry _registry;

        public CommandProcessor(ICommandHandlerRegistry registry, Func<IBusContext> busContextFactory, ITextSerializer serializer)
            : base(busContextFactory, serializer)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));
            _registry = registry;
        }

        protected override async Task ProcessMessageAsync(object message, string correlationId)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var commandType = message.GetType();
            ICommandHandler handler;

            if (_registry.TryGetHandler(commandType, out handler))
            {
                Logging.DarjeelEntityFramework.TraceInformation($"Command '{commandType.FullName}' handled by '{handler.GetType().FullName}.");
                await ((dynamic)handler).HandleAsync((dynamic)message);
            }
            else if (_registry.TryGetHandler(typeof(ICommand), out handler))
            {
                Logging.DarjeelEntityFramework.TraceInformation($"Command '{commandType.FullName}' handled by '{handler.GetType().FullName}.");
                await ((dynamic)handler).HandleAsync((dynamic)message);
            }
        }
    }
}