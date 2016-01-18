namespace Darjeel.Messaging.Handling
{
    public interface IRegistry<in THandler>
    {
        void Register(THandler handler);
    }
}