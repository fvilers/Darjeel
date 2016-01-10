using System;

namespace Darjeel.Infrastructure.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}