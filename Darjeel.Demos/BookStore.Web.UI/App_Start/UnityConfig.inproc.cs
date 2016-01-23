using Darjeel.Memory.Messaging;
using Darjeel.Messaging;
using Microsoft.Practices.Unity;
using System;

namespace BookStore.Web.UI
{
    public partial class UnityConfig
    {
        private static void RegisterInProcCommandBusAndProcessor(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            container.RegisterType<ICommandBus, InProcCommandBus>();
        }

        private static void RegisterInProcEventBusAndProcessor(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            container.RegisterType<IEventBus, InProcEventBus>();
        }
    }
}