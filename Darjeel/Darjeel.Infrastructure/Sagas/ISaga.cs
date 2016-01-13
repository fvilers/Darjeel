using Darjeel.Infrastructure.Messaging;
using System;
using System.Collections.Generic;

namespace Darjeel.Infrastructure.Sagas
{
    public interface ISaga
    {
        Guid Id { get; }
        IEnumerable<Envelope<ICommand>> Commands { get; }
    }
}