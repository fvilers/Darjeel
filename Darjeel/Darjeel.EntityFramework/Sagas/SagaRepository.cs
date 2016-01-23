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
        private readonly Func<ISagaContext> _contextFactory;
        private readonly ICommandBus _commandBus;

        public SagaRepository(Func<ISagaContext> contextFactory, ICommandBus commandBus)
        {
            if (contextFactory == null) throw new ArgumentNullException(nameof(contextFactory));
            if (commandBus == null) throw new ArgumentNullException(nameof(commandBus));
            _contextFactory = contextFactory;
            _commandBus = commandBus;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            using (var context = _contextFactory())
            {
                var saga = await context.Set<T>().FirstOrDefaultAsync(predicate);

                return saga;
            }
        }

        public async Task StoreAsync(T saga)
        {
            if (saga == null) throw new ArgumentNullException(nameof(saga));

            using (var context = _contextFactory())
            {
                if (context.IsDetached(saga))
                {
                    Logging.DarjeelEntityFramework.TraceInformation($"Attaching saga {saga.Id} to its context because it is currently detached.");
                    context.Set<T>().Add(saga);
                }

                await context.SaveChangesAsync();
            }

            await _commandBus.SendAsync(saga.Commands);
        }
    }
}
