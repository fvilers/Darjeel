using Darjeel.Processors;
using Microsoft.Practices.Unity;
using System;
using System.Web.Hosting;

namespace BookStore.Web.UI
{
    public static class ProcessorConfig
    {
        public static void Start(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            foreach (var processor in container.ResolveAll<IProcessor>())
            {
                HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => processor.Start(cancellationToken));
            }
        }
    }
}