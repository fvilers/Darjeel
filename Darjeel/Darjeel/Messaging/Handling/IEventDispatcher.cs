using System.Threading.Tasks;

namespace Darjeel.Messaging.Handling
{
    public interface IEventDispatcher
    {
        Task DispatchEventAsync(IEvent @event, string correlationId = null);
    }
}