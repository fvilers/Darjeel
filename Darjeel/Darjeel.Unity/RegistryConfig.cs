using Darjeel.Messaging.Handling;
using Microsoft.Practices.Unity;
using System;

namespace Darjeel.Unity
{
    public static class RegistryConfig
    {
        public static void RegisterCommandHandlers(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            var registry = container.Resolve<ICommandHandlerRegistry>();

            if (registry == null)
            {
                throw new Exception("No command handler registry has been registed within the given container.");
            }

            foreach (var handler in container.ResolveAll<ICommandHandler>())
            {
                registry.Register(handler);
            }
        }

        public static void RegisterEventHandlers(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            var registry = container.Resolve<IEventHandlerRegistry>();

            if (registry == null)
            {
                throw new Exception("No event handler registry has been registed within the given container.");
            }

            foreach (var handler in container.ResolveAll<IEventHandler>())
            {
                registry.Register(handler);
            }
        }
    }
}