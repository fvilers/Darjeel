using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync(Envelope<IEvent> envelope);
        Task PublishAsync(IEnumerable<Envelope<IEvent>> envelopes);
    }
}