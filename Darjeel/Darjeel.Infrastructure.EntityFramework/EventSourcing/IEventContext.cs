using Darjeel.Infrastructure.EventSourcing;
using System.Data.Entity;

namespace Darjeel.Infrastructure.EntityFramework.EventSourcing
{
    public interface IEventContext : IContext
    {
        IDbSet<StoredEvent> Events { get; }
    }
}