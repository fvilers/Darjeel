using Darjeel.EventSourcing;
using System.Data.Entity;

namespace Darjeel.EntityFramework.EventSourcing
{
    public interface IEventContext : IContext
    {
        IDbSet<StoredEvent> Events { get; }
    }
}