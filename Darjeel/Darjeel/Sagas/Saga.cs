using Darjeel.Messaging;
using System;
using System.Collections.Generic;

namespace Darjeel.Sagas
{
    public abstract class Saga : ISaga
    {
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public Guid Id { get; private set; } // Id has a private setter because EF requires Id to be a scalar type
        public IEnumerable<Envelope<ICommand>> Commands => _commands;

        private readonly List<Envelope<ICommand>> _commands = new List<Envelope<ICommand>>();

        protected Saga()
        {
            Id = Guid.NewGuid();
        }

        protected void AddCommand<T>(T command)
            where T : ICommand
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            AddCommand(new Envelope<ICommand>(command));
        }

        protected void AddCommand(Envelope<ICommand> command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            _commands.Add(command);
        }
    }
}