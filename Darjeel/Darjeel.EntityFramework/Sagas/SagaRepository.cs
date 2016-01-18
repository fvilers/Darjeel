using Darjeel.Messaging;
using Darjeel.Sagas;
using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Darjeel.EntityFramework.Sagas
{
    public class SagaRepository<T> : ISagaRepository<T>
        where T : class, ISaga
    {
        private readonly ISagaContext _context;
        private readonly ICommandBus _commandBus;

        public SagaRepository(ISagaContext context, ICommandBus commandBus)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (commandBus == null) throw new ArgumentNullException(nameof(commandBus));
            _context = context;
            _commandBus = commandBus;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var saga = await _context.Set<T>().FirstOrDefaultAsync(predicate);

            return saga;
        }

        public async Task StoreAsync(T saga)
        {
            if (saga == null) throw new ArgumentNullException(nameof(saga));

            if (_context.IsDetached(saga))
            {
                Logging.DarjeelEntityFramework.TraceInformation($"Attaching saga {saga.Id} to its context because it is currently detached.");
                _context.Set<T>().Add(saga);
            }

            await _context.SaveChangesAsync();
            await _commandBus.SendAsync(saga.Commands);
        }
    }
}
