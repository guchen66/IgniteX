using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace IgniteX.Shared.Extensions
{
    public static class JsonExtension
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        public static readonly JsonSerializerSettings JsonDeserializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
    //    public static readonly FileLocatorConverter FileLocatorConverter = new FileLocatorConverter();

        static JsonExtension()
        {
          //  JsonSerializerSettings.Converters.Add(FileLocatorConverter);
        }

        public static string ToJson<T>(this T @object, Formatting formatting = Formatting.None, bool specifyRootType = true)
        {
            var type = @object.GetType();

            return typeof(T) != type && specifyRootType
                ? JsonConvert.SerializeObject(@object, typeof(T), formatting, JsonSerializerSettings)
                : JsonConvert.SerializeObject(@object, formatting, JsonSerializerSettings);
        }

        public static T ToObject<T>(this string json)
        {
            return !string.IsNullOrWhiteSpace(json)
                ? JsonConvert.DeserializeObject<T>(json, JsonDeserializerSettings)
            : default;
        }
    }

   /* public class FileLocatorConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var fileLocator = (FileLocator)value;
            writer.WriteValue(fileLocator.FullPath);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(FileLocator);
    }*/
}
