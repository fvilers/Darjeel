using System;
using System.Threading.Tasks;

namespace Darjeel.Messaging.Extensions
{
    public static class CommandBusExtensions
    {
        public static Task SendAsync(this ICommandBus commandBus, ICommand command)
        {
            if (commandBus == null) throw new ArgumentNullException(nameof(commandBus));
            if (command == null) throw new ArgumentNullException(nameof(command));

            return commandBus.SendAsync(new Envelope<ICommand>(command));
        }
    }
}
