using Darjeel.Messaging;
using System;
using System.Collections.Generic;

namespace Darjeel.Sagas
{
    public interface ISaga
    {
        Guid Id { get; }
        IEnumerable<Envelope<ICommand>> Commands { get; }
    }
}