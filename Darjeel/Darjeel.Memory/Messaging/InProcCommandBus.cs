using Darjeel.Messaging;
using Darjeel.Messaging.Handling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Memory.Messaging
{
    public class InProcCommandBus : ICommandBus
    {
        private readonly ICommandExecuter _executer;

        public InProcCommandBus(ICommandExecuter executer)
        {
            if (executer == null) throw new ArgumentNullException(nameof(executer));
            _executer = executer;
        }

        public async Task SendAsync(Envelope<ICommand> envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            await _executer.ExecuteAsync(envelope.Body, envelope.CorrelationId);
        }

        public async Task SendAsync(IEnumerable<Envelope<ICommand>> envelopes)
        {
            if (envelopes == null) throw new ArgumentNullException(nameof(envelopes));

            foreach (var envelope in envelopes)
            {
                await SendAsync(envelope);
            }
        }
    }
}