using System.Threading;

namespace Darjeel.Processors
{
    public interface IProcessor
    {
        void Start(CancellationToken cancellationToken);
    }
}