using Darjeel.Infrastructure.Messaging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.Memory.Messaging
{
    public class InMemoryCommandBus : ICommandBus
    {
        private readonly IProducerConsumerCollection<Envelope<ICommand>> _commandCollection;

        public InMemoryCommandBus(IProducerConsumerCollection<Envelope<ICommand>> commandCollection)
        {
            if (commandCollection == null) throw new ArgumentNullException(nameof(commandCollection));
            _commandCollection = commandCollection;
        }

        public Task SendAsync(Envelope<ICommand> envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (!_commandCollection.TryAdd(envelope))
            {
                throw new Exception("Sending envelope failed.");
            }

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
