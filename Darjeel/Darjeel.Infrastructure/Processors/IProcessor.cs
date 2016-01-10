using System.Threading;

namespace Darjeel.Infrastructure.Processors
{
    public interface IProcessor
    {
        void Start(CancellationToken cancellationToken);
    }
}