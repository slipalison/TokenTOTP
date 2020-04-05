using System.Collections.Generic;

namespace Responses
{
    public class AggregatedError : IError
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public LayerEnum Layer { get; set; }

        public string ApplicationName { get; set; }

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