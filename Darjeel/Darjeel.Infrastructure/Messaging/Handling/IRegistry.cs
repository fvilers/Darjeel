namespace Darjeel.Infrastructure.Messaging.Handling
{
    public interface IRegistry<in THandler>
    {
        void Register(THandler handler);
    }
}