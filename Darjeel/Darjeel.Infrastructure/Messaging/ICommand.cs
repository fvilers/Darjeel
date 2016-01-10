using System;

namespace Darjeel.Infrastructure.Messaging
{
    public interface ICommand : IMessage
    {
        Guid Id { get; }
    }
}