using Darjeel.Infrastructure.EntityFramework.Messaging;
using Darjeel.Infrastructure.Processors;
using Darjeel.Infrastructure.Serialization;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.EntityFramework.Processors
{
    public abstract class MessageProcessor<T> : IProcessor
        where T : MessageEntity
    {
        private readonly Func<IBusContext> _busContextFactory;
        private readonly ITextSerializer _serializer;
        private readonly object _lock = new object();
        private bool _isRunning;

        protected MessageProcessor(Func<IBusContext> busContextFactory, ITextSerializer serializer)
        {
            if (busContextFactory == null) throw new ArgumentNullException(nameof(busContextFactory));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            _busContextFactory = busContextFactory;
            _serializer = serializer;
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

        protected abstract Task ProcessMessageAsync(object message, string correlationId);

        private async Task StartPollingAsync(CancellationToken cancellationToken)
        {
            var pollDelay = TimeSpan.FromMilliseconds(250);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var message = await TryGetMessageAsync(cancellationToken);
                    if (message != null)
                    {
                        var body = Deserialize(message.Body);

                        TracePayload(body);
                        await ProcessMessageAsync(body, message.CorrelationId);
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError("An exception happened while processing message through handler/s:\r\n{0}.", e);
                    Trace.TraceWarning("Error will be ignored and message receiving will continue.");
                    Debugger.Break();
                }

                await Task.Delay(pollDelay, cancellationToken);
            }
        }

        private async Task<T> TryGetMessageAsync(CancellationToken cancellationToken)
        {
            using (var context = _busContextFactory())
            {
                using (var trans = context.BeginTransaction())
                {
                    var now = DateTime.UtcNow;
                    var query = from x in context.Set<T>()
                                where !x.DeliveryDate.HasValue || x.DeliveryDate.Value <= now
                                select x;
                    var message = await query.FirstOrDefaultAsync(cancellationToken);

                    if (message != null)
                    {
                        context.Set<T>().Remove(message);
                        await context.SaveChangesAsync();
                    }

                    trans.Commit();

                    return message;
                }
            }
        }

        private object Deserialize(string serialized)
        {
            using (var reader = new StringReader(serialized))
            {
                return _serializer.Deserialize(reader);
            }
        }

        private string Serialize(object payload)
        {
            using (var writer = new StringWriter())
            {
                _serializer.Serialize(writer, payload);
                return writer.ToString();
            }
        }

        [Conditional("TRACE")]
        private void TracePayload(object payload)
        {
            Trace.WriteLine(Serialize(payload));
        }
    }
}
