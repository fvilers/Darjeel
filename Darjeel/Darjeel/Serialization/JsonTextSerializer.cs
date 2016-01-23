using Darjeel.Diagnostics.Extensions;
using Darjeel.Extensions;
using Darjeel.Serialization.Extensions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;

namespace Darjeel.Serialization
{
    public class JsonTextSerializer : ITextSerializer
    {
        private readonly JsonSerializer _serializer;

        public JsonTextSerializer()
            : this(CreateSerializer())
        {
        }

        private JsonTextSerializer(JsonSerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            _serializer = serializer;
        }

        public void Serialize(TextWriter writer, object graph)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var jsonWriter = new JsonTextWriter(writer);

            _serializer.Serialize(jsonWriter, graph);
            writer.Flush();
        }

        public object Deserialize(TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            var jsonReader = new JsonTextReader(reader);

            try
            {
                return _serializer.Deserialize(jsonReader);
            }
            catch (JsonSerializationException e)
            {
                Logging.Darjeel.TraceError($"Unable to deserialize reader.\r\n{e.AsJson()}");
                throw new SerializationException("Unable to deserialize reader.", e);
            }
        }

        private static JsonSerializer CreateSerializer()
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            };

            settings.EnsureFormatting();

            var serializer = JsonSerializer.Create(settings);

            return serializer;
        }
    }
}