using System.IO;

namespace MessageBus.Serializers
{
    internal sealed class ProtobufSerializer : Serializer
    {
        internal override string ContentType => "application/protobuf";

        public override T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default(T);

            using (var stream = new MemoryStream(data))
                return ProtoBuf.Serializer.Deserialize<T>(stream);
        }

        public override byte[] Serialize<T>(T obj)
        {
            if (obj == null)
                return new byte[] { };

            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, obj);
                return stream.ToArray();
            }
        }
    }
}