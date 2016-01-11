using Darjeel.Infrastructure.Messaging;
using Darjeel.Infrastructure.Processors;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.Memory.Processors
{
    public abstract class MessageProcessor<T> : IProcessor
        where T : IMessage
    {
        private readonly IProducerConsumerCollection<Envelope<T>> _commandCollection;
        private readonly object _lock = new object();
        private bool _isRunning;

        protected MessageProcessor(IProducerConsumerCollection<Envelope<T>> commandCollection)
        {
            if (commandCollection == null) throw new ArgumentNullException(nameof(commandCollection));
            _commandCollection = commandCollection;
        }

        public void Start(CancellationToken cancellationToken)
        {
            lock (_lock)
            {
                if (!_isRunning)
                {
                    Task.Factory.StartNew(async () => await StartPollingAsync(cancellationToken), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);
                    _isRunning = true;
                }
            }
        }

        protected abstract void ProcessMessage(T message, string correlationId);

        private async Task StartPollingAsync(CancellationToken cancellationToken)
        {
            var pollDelay = TimeSpan.FromMilliseconds(250);

            while (!cancellationToken.IsCancellationRequested)
            {
                Envelope<T> envelope;
                if (_commandCollection.TryTake(out envelope))
                {
                    ProcessMessage(envelope.Body, envelope.CorrelationId);
                }

                await Task.Delay(pollDelay, cancellationToken);
            }
        }
    }
}
