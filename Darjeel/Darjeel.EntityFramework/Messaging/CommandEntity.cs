using System;

namespace Darjeel.EntityFramework.Messaging
{
    public class CommandEntity : MessageEntity
    {
        private CommandEntity()
        {
            // Required for Entity Framework
        }

        public CommandEntity(string body, DateTime? deliveryDate = null, string correlationId = null)
            : base(body, deliveryDate, correlationId)
        {
        }
    }
}