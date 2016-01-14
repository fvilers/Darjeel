using System.Data.Entity;

namespace Darjeel.Infrastructure.EntityFramework.Messaging
{
    public interface IBusContext : IContext
    {
        IDbSet<CommandEntity> Commands { get; }
        IDbSet<EventEntity> Events { get; }
    }
}