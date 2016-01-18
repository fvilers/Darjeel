using System.Threading.Tasks;

namespace Darjeel.Messaging.Handling
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in T> : ICommandHandler
        where T : ICommand
    {
        Task HandleAsync(T command);
    }
}