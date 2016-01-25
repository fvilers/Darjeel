using Darjeel.Diagnostics.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Darjeel.Messaging.Handling
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        private readonly Dictionary<Type, ICommandHandler> _handlers = new Dictionary<Type, ICommandHandler>();

        public void Register(ICommandHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var supportedCommandTypes = handler.GetType()
                .GetInterfaces()
                .Where(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                .Select(iface => iface.GetGenericArguments()[0])
                .ToList();

            if (_handlers.Keys.Any(registeredType => supportedCommandTypes.Contains(registeredType)))
            {
                Logging.Darjeel.TraceError("The command handled by the received handler already has a registered handler.");
                throw new ArgumentException("The command handled by the received handler already has a registered handler.");
            }

            foreach (var commandType in supportedCommandTypes)
            {
                _handlers.Add(commandType, handler);
            }
        }

        public bool TryGetHandler(Type commandType, out ICommandHandler handler)
        {
            if (_handlers.TryGetValue(commandType, out handler))
            {
                return true;
            }

            Logging.Darjeel.TraceInformation($"Handler not found for command '{commandType.FullName}'.");

            return false;
        }
    }
}