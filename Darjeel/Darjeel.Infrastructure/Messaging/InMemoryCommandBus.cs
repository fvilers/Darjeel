using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.Messaging
{
    public class InMemoryCommandBus : ICommandBus
    {
        private readonly Queue<Envelope<ICommand>> _queue = new Queue<Envelope<ICommand>>();

        public Task SendAsync(Envelope<ICommand> envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            _queue.Enqueue(envelope);

            return Task.FromResult(0);
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
