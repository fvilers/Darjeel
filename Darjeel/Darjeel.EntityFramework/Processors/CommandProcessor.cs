using Darjeel.EntityFramework.Messaging;
using Darjeel.Messaging;
using Darjeel.Messaging.Handling;
using Darjeel.Serialization;
using System;
using System.Threading.Tasks;

namespace Darjeel.EntityFramework.Processors
{
    public class CommandProcessor : MessageProcessor<CommandEntity>
    {
        private readonly ICommandExecuter _executer;

        public CommandProcessor(ICommandExecuter executer, Func<IBusContext> busContextFactory, ITextSerializer serializer)
            : base(busContextFactory, serializer)
        {
            if (executer == null) throw new ArgumentNullException(nameof(executer));
            _executer = executer;
        }

        protected override async Task ProcessMessageAsync(object message, string correlationId)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            await _executer.ExecuteAsync((ICommand)message, correlationId);
        }
    }
}