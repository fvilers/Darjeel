using System.Threading.Tasks;

namespace Darjeel.Messaging.Handling
{
    public interface ICommandExecuter
    {
        Task ExecuteAsync(ICommand command, string correlationId = null);
    }
}