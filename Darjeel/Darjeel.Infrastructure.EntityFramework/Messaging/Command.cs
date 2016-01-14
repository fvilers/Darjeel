using System;

namespace Darjeel.Infrastructure.EntityFramework.Messaging
{
    public class Command : Message
    {
        private Command()
        {
            // Required for Entity Framework
        }

        public Command(string body, DateTime? deliveryDate = null, string correlationId = null)
            : base(body, deliveryDate, correlationId)
        {
        }
    }
}