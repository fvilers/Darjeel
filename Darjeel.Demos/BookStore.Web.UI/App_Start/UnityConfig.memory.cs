using Darjeel.Infrastructure.Memory.Messaging;
using Darjeel.Infrastructure.Memory.Processors;
using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Processors;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Concurrent;

namespace BookStore.Web.UI
{
    public partial class UnityConfig
    {
        private static void RegisterMemoryCommandBusAndProcessor(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            container.RegisterInstance<IProducerConsumerCollection<Envelope<ICommand>>>(new ConcurrentQueue<Envelope<ICommand>>());
            container.RegisterType<IProcessor, CommandProcessor>("CommandProcessor");
            container.RegisterType<ICommandBus, CommandBus>();
        }

        private static void RegisterMemoryEventBusAndProcessor(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            container.RegisterInstance<IProducerConsumerCollection<Envelope<IEvent>>>(new ConcurrentQueue<Envelope<IEvent>>());
            container.RegisterType<IProcessor, EventProcessor>("EventProcessor");
            container.RegisterType<IEventBus, EventBus>();
        }
    }
}