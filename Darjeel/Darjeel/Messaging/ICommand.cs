using System;

namespace Darjeel.Messaging
{
    public interface ICommand : IMessage
    {
        Guid Id { get; }
    }
}