using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Darjeel
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public Guid EntityId { get; }
        public string EntityType { get; }

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(Guid entityId)
            : base(entityId.ToString())
        {
            EntityId = entityId;
        }

        public EntityNotFoundException(Guid entityId, Type entityType)
            : base(entityType + " : " + entityId)
        {
            EntityId = entityId;
            EntityType = entityType.ToString();
        }

        public EntityNotFoundException(Guid entityId, Type entityType, string message, Exception inner)
            : base(message, inner)
        {
            EntityId = entityId;
            EntityType = entityType.ToString();
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            EntityId = Guid.Parse(info.GetString("entityId"));
            EntityType = info.GetString("entityType");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("entityId", EntityId.ToString());
            info.AddValue("entityType", EntityType);
        }
    }
}