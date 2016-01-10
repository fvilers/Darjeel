using System.Collections.Generic;
using System.Threading.Tasks;

namespace Darjeel.Infrastructure.Messaging
{
    public interface ICommandBus
    {
        Task SendAsync(Envelope<ICommand> envelope);
        Task SendAsync(IEnumerable<Envelope<ICommand>> envelopes);
    }
}