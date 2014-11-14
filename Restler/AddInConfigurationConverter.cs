using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Restler
{
    public class AddInConfigurationConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer,value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var jObject = JObject.Load(reader);
            return jObject.ToString(Formatting.None);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
