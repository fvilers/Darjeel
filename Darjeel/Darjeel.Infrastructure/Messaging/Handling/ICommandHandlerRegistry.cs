using System;

namespace Darjeel.Infrastructure.Messaging.Handling
{
    public interface ICommandHandlerRegistry : IRegistry<ICommandHandler>
    {
        bool TryGetHandler(Type commandType, out ICommandHandler handler);
    }
}