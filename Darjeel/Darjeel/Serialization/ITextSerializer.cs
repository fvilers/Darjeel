using System.IO;

namespace Darjeel.Serialization
{
    public interface ITextSerializer
    {
        void Serialize(TextWriter writer, object graph);
        object Deserialize(TextReader reader);
    }
}