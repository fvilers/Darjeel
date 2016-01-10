using System;

namespace Darjeel.Infrastructure.Messaging
{
    public class Envelope<T> where T : IMessage
    {
        public T Body { get; private set; }
        public TimeSpan Delay { get; set; }
        public string CorrelationId { get; set; }

        public Envelope(T body)
        {
            Body = body;
        }
    }
}