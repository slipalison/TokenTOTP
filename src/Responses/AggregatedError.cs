using ProtoBuf;
using System.Collections.Generic;

namespace Responses
{
    [ProtoContract]
    public class AggregatedError : IError
    {
        [ProtoMember(1)]
        public string Code { get; set; }

        [ProtoMember(2)]
        public string Message { get; set; }

        [ProtoMember(3)]
        public LayerEnum Layer { get; set; }

        [ProtoMember(4)]
        public string ApplicationName { get; set; }

        [ProtoMember(5)]
        public IEnumerable<KeyValuePair<string, string>> Errors { get; set; }

        public AggregatedError()
        {
            Layer = ResultContext.Layer;
            ApplicationName = ResultContext.ApplicationName;
        }

        public AggregatedError(string code, string message)
        {
            Code = code;
            Message = message;
            Layer = ResultContext.Layer;
            ApplicationName = ResultContext.ApplicationName;
        }

        public AggregatedError((string code, string message) error)
        {
            Code = error.code;
            Message = error.message;
            Layer = ResultContext.Layer;
            ApplicationName = ResultContext.ApplicationName;
        }
    }
}