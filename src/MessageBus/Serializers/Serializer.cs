using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageBus.Serializers
{
    internal abstract class Serializer
    {
        internal static readonly Serializer Protobuf = new ProtobufSerializer();
        internal static readonly Serializer Json = new JsonSerializer();

        private static readonly IEnumerable<Serializer> _serializer;

        static Serializer()
        {
            _serializer = new Serializer[]
            {
                Protobuf,
                Json
            };
        }

        internal abstract string ContentType { get; }

        public abstract byte[] Serialize<T>(T obj);

        public abstract T Deserialize<T>(byte[] data);

        public static Serializer Get(string contentType) =>
            _serializer.FirstOrDefault(it => it.ContentType.Equals(GetValueOrDefault(contentType), StringComparison.InvariantCultureIgnoreCase));

        private static string GetValueOrDefault(string accept) =>
            !string.IsNullOrWhiteSpace(accept) ? accept : Protobuf.ContentType;
    }
}