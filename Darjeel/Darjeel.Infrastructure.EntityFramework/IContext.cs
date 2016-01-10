using Darjeel.Infrastructure.Persistence;
using System;

namespace Darjeel.Infrastructure.EntityFramework
{
    public interface IContext : IUnitOfWork, IDisposable
    {
    }
}