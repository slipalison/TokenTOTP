using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Diagnostics;

namespace Responses
{
    internal static class ResultMessages
    {
        public static readonly string ErrorMessageToSuccess = "There is no error message for success.";

        public static readonly string ErrorMessageIsNotProvidedForFailure = "There must be errormessage for failure.";

        public static readonly string ValueToFailure = "There is no value for failure.";
    }

    [ProtoContract]
    public class Result
    {
        private Result()
        {
        }

        [JsonProperty(nameof(Error))]
        private Error _error;

        [JsonIgnore]
        [ProtoMember(1)]
        public Error Error
        {
            get
            {
                if (IsSuccess)
                    throw new InvalidOperationException(ResultMessages.ErrorMessageToSuccess);

                return _error;
            }
            private set => _error = value;
        }

        [ProtoMember(2)]
        public bool IsSuccess { get; private set; }

        protected Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            _error = error;
        }

        protected Result(string code, string message)
        {
            IsSuccess = false;
            _error = new Error(code, message);
        }

        protected Result((string code, string message) error)
        {
            IsSuccess = false;
            _error = new Error(error.code, error.message);
        }

        [DebuggerStepThrough]
        public static Result Ok()
        {
            return new Result(true, null);
        }

        [DebuggerStepThrough]
        public static Result Fail(string code, string message)
        {
            return new Result(code, message);
        }

        [DebuggerStepThrough]
        public static Result Fail((string, string) item)
        {
            return Fail(item.ToTuple());
        }

        [DebuggerStepThrough]
        public static Result Fail(Tuple<string, string> item)
        {
            return new Result(item.Item1, item.Item2);
        }

        [DebuggerStepThrough]
        public static Result Fail(Error error)
        {
            return new Result(false, error);
        }

        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(true, null, value);
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string code, string message)
        {
            return new Result<T>(false, new Error(code, message), default(T));
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>((string, string) item)
        {
            return Fail<T>(item.ToTuple());
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(Tuple<string, string> item)
        {
            return new Result<T>(false, new Error(item.Item1, item.Item2), default(T));
        }

        [DebuggerStepThrough]
        public static Result<T> Fail<T>(Error error)
        {
            return new Result<T>(false, error, default(T));
        }

        [DebuggerStepThrough]
        public static Result<TValue, TError> Ok<TValue, TError>(TValue value) where TError : IError
        {
            return new Result<TValue, TError>(true, default(TError), value);
        }

        [DebuggerStepThrough]
        public static Result<TValue, TError> Fail<TValue, TError>(TError error) where TError : IError
        {
            return new Result<TValue, TError>(false, error, default(TValue));
        }
    }

    [ProtoContract]
    public class Result<T>
    {
        private Result()
        {
        }

        [JsonProperty(nameof(Error))]
        [ProtoMember(1)]
        private Error _error;

        [JsonIgnore]
        public Error Error
        {
            get
            {
                if (IsSuccess)
                    throw new InvalidOperationException(ResultMessages.ErrorMessageToSuccess);

                return _error;
            }
            private set => _error = value;
        }

        [ProtoMember(2)]
        [JsonProperty]
        public bool IsSuccess { get; private set; }

        [ProtoMember(3)]
        [JsonProperty(nameof(Value))]
        private T _value;

        [JsonIgnore]
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException(ResultMessages.ValueToFailure);

                return _value;
            }
            private set => _value = value;
        }

        internal Result(bool isSuccess, Error error, T value)
        {
            IsSuccess = isSuccess;
            _error = error;
            _value = value;
        }
    }

    [ProtoContract]
    public class Result<TValue, TError> where TError : IError
    {
        private Result()
        {
        }

        [ProtoMember(1)]
        [JsonProperty(nameof(Error))]
        private TError _error;

        [JsonIgnore]
        public TError Error
        {
            get
            {
                if (IsSuccess)
                    throw new InvalidOperationException(ResultMessages.ErrorMessageToSuccess);

                return _error;
            }
            private set => _error = value;
        }

        [ProtoMember(2)]
        [JsonProperty]
        public bool IsSuccess { get; private set; }

        [ProtoMember(3)]
        [JsonProperty(nameof(Value))]
        private TValue _value;

        [JsonIgnore]
        public TValue Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException(ResultMessages.ValueToFailure);

                return _value;
            }
            private set => _value = value;
        }

        internal Result(bool isSuccess, TError error, TValue value)
        {
            IsSuccess = isSuccess;
            _error = error;
            _value = value;
        }
    }
}