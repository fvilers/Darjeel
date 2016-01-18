using System;

namespace Darjeel.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}