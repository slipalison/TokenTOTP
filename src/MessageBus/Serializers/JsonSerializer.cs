using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Text;

namespace MessageBus.Serializers
{
    internal sealed class JsonSerializer : Serializer
    {
        internal override string ContentType => "application/json";

        private readonly JsonSerializerSettings _settings;
        public JsonSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                },
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public override T Deserialize<T>(byte[] data)
            => data == null ? default(T) : JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data), _settings);
        public override byte[] Serialize<T>(T obj) =>
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, _settings));
    }
}