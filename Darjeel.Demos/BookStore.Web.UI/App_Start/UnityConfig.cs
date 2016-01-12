using BookStore.Catalog.CommandHandlers;
using BookStore.Catalog.Persistence.ReadModels;
using BookStore.Catalog.ReadModels;
using Darjeel.Infrastructure.Memory.Messaging;
using Darjeel.Infrastructure.Memory.Processors;
using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Messaging.Handling;
using Darjeel.Infrastructure.Processors;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Concurrent;

namespace BookStore.Web.UI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // Catalog command handlers
            container.RegisterType<ICommandHandler, ProductCommandHandler>("ProductCommandHandler");

            // Catalog read models
            container.RegisterType<IReadModelContext, ReadModelContext>();
            container.RegisterType<IReadModelProductDao, ReadModelProductDao>();

            // Infrastructure
            container.RegisterType<ICommandHandlerRegistry, CommandHandlerRegistry>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<IProducerConsumerCollection<Envelope<ICommand>>>(new ConcurrentQueue<Envelope<ICommand>>());
            container.RegisterType<IProcessor, CommandProcessor>("CommandProcessor");
            container.RegisterType<ICommandBus, CommandBus>();

            container.RegisterType<IEventHandlerRegistry, EventHandlerRegistry>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<IProducerConsumerCollection<Envelope<IEvent>>>(new ConcurrentQueue<Envelope<IEvent>>());
            container.RegisterType<IProcessor, EventProcessor>("EventProcessor");
            container.RegisterType<IEventBus, EventBus>();
        }
    }
}
