using Darjeel.Unity.Extensions;
using Microsoft.Practices.Unity;
using System;

namespace BookStore.Web.UI
{
    public static class RegistryConfig
    {
        public static void Register(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            container.RegisterCommandHandlers().RegisterEventHandlers();
        }
    }
}