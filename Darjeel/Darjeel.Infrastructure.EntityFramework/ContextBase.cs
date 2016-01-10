using System.Data.Entity;

namespace Darjeel.Infrastructure.EntityFramework
{
    public abstract class ContextBase : DbContext, IContext
    {
        protected ContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
    }
}