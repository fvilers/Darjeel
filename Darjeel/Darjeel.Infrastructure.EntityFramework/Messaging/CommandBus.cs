using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.EntityFramework.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<IBusContext> _contextFactory;
        private readonly ITextSerializer _serializer;

        public CommandBus(Func<IBusContext> contextFactory, ITextSerializer serializer)
        {
            if (contextFactory == null) throw new ArgumentNullException(nameof(contextFactory));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            _contextFactory = contextFactory;
            _serializer = serializer;
        }

        public async Task SendAsync(Envelope<ICommand> envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            var message = BuildCommand(envelope);

            using (var context = _contextFactory())
            {
                context.Commands.Add(message);
                await context.SaveChangesAsync();
            }
        }

        public async Task SendAsync(IEnumerable<Envelope<ICommand>> envelopes)
        {
            if (envelopes == null) throw new ArgumentNullException(nameof(envelopes));

            using (var context = _contextFactory())
            {
                foreach (var message in envelopes.Select(BuildCommand))
                {
                    context.Commands.Add(message);
                }

                await context.SaveChangesAsync();
            }
        }

        private CommandEntity BuildCommand(Envelope<ICommand> @event)
        {
            using (var payloadWriter = new StringWriter())
            {
                _serializer.Serialize(payloadWriter, @event.Body);

                return new CommandEntity(payloadWriter.ToString(), correlationId: @event.CorrelationId);
            }
        }
    }
}
