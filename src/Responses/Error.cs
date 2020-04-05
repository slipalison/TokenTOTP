using ProtoBuf;
using System;

namespace Responses
{
    [ProtoContract]
    public class Error : IError
    {
        [ProtoMember(1)]
        public string Code { get; set; }

        [ProtoMember(2)]
        public string Message { get; set; }

        [ProtoMember(3)]
        public LayerEnum Layer { get; set; }

        [ProtoMember(4)]
        public string ApplicationName { get; set; }

        public Error(string code, string message)
        {
            ValidateCtor(code, message);

            Code = code;
            Message = message;
            Layer = ResultContext.Layer;
            ApplicationName = ResultContext.ApplicationName;
        }

        public Error((string code, string message) error)
        {
            ValidateCtor(error.code, error.message);

            Code = error.code;
            Message = error.message;
            Layer = ResultContext.Layer;
            ApplicationName = ResultContext.ApplicationName;
        }

        public Error()
        {
            Layer = ResultContext.Layer;
            ApplicationName = ResultContext.ApplicationName;
        }

        private static void ValidateCtor(string code, string message)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(code));
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(ApplicationName))
                throw new ArgumentNullException(nameof(ApplicationName));

            if (Layer == LayerEnum.None)
                throw new ArgumentException(nameof(Layer));

            return $"[{Layer}] {ApplicationName} - {Code}: {Message}";
        }
    }
}