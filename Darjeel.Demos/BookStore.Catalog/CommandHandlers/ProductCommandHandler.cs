using BookStore.Catalog.Commands;
using Darjeel.Infrastructure.Messaging.Handling;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BookStore.Catalog.CommandHandlers
{
    public class ProductCommandHandler : ICommandHandler<CreateProduct>
    {
        public Task HandleAsync(CreateProduct command)
        {
            Trace.TraceError("Create product handler not implemented.");

            return Task.FromResult(0);
        }
    }
}
