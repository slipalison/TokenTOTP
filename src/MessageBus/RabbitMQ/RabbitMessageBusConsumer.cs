using MessageBus.Options;
using MessageBus.Serializers;
using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBus.RabbitMQ
{
    public sealed class RabbitMessageBusConsumer<T> : RabbitMessageBusPublisher<T>, IMessageBusConsumer<T> where T : class
    {
        private const int MaxLimitCount = 2;
        private readonly ILogger<RabbitMessageBusConsumer<T>> _logger;

        private IDisposable _consumer;
        private Func<T, MessageProperties, CancellationTokenSource, Task> _onProcess;
        private Func<T, MessageProperties, Task> _onCancel;
        private Action<T, MessageProperties, Exception> _onError;

        public RabbitMessageBusConsumer(IAdvancedBus bus, ExchangeConfiguration exchangeConfiguration, QueueConfiguration queueConfiguration, ILogger<RabbitMessageBusConsumer<T>> logger, MessageBusOptions options)
            : base(bus, exchangeConfiguration, queueConfiguration, options)
        {
            _logger = logger;

            ConfigureBusEvents();
        }

        public void RegisterHandler(Func<T, MessageProperties, CancellationTokenSource, Task> onProcess) => this.RegisterHandler(onProcess, OnCancel, OnError);

        public void RegisterHandler(Func<T, MessageProperties, CancellationTokenSource, Task> onProcess, Action<T, MessageProperties, Exception> onError) => this.RegisterHandler(onProcess, OnCancel, onError);

        public void RegisterHandler(Func<T, MessageProperties, CancellationTokenSource, Task> onProcess, Func<T, MessageProperties, Task> onCancel) => this.RegisterHandler(onProcess, onCancel, OnError);

        public void RegisterHandler(Func<T, MessageProperties, CancellationTokenSource, Task> onProcess, Func<T, MessageProperties, Task> onCancel, Action<T, MessageProperties, Exception> onError)
        {
            _onProcess = onProcess;
            _onCancel = onCancel;
            _onError = onError;
            _onError += (m, p, e) => MoveToDeadLetter(m, p, e);

            Start();
        }

        private void Start() => _consumer = Bus.Consume(new Queue(QueueConfiguration.Name, true), async (message, properties, info) => await RunAsync(message, properties, info));

        private void Stop() => _consumer?.Dispose();

        private void ConfigureBusEvents()
        {
            Bus.Connected += (sender, args) => Start();
            Bus.Disconnected += (sender, args) => Stop();
        }

        private async Task RunAsync(byte[] message, MessageProperties properties, MessageReceivedInfo info)
        {
            var deserializedMessage = Serializer.Get(properties.ContentType).Deserialize<T>(message);

            try
            {
                var tokenSource = new CancellationTokenSource();

                if (!(_onProcess is null))
                    await _onProcess(deserializedMessage, properties, tokenSource);

                if (tokenSource.IsCancellationRequested)
                    await _onCancel(deserializedMessage, properties);
            }
            catch (Exception ex)
            {
                _onError?.Invoke(deserializedMessage, properties, ex);
            }
        }

        private void OnError(T message, MessageProperties props, Exception ex)
        {
            _logger.LogError(ex, "Unable to process message");
            _logger.LogInformation($"Content type: { props.ContentType }");
            _logger.LogInformation($"Received payload: { message }");
        }

        private async Task OnCancel(T message, MessageProperties props)
        {
            var count = GetMaxDeliveryCount(props);
            if (count > MaxLimitCount) { throw new Exception("Exceeded max delivery count limit"); }

            props.Headers.Remove("max-delivery-count");
            props.Headers.Add("max-delivery-count", ++count);

            await PublishAsync(message, props);
        }

        private static int GetMaxDeliveryCount(MessageProperties props)
        {
            props.Headers.TryGetValue("max-delivery-count", out var value);
            var count = (int?)value ?? 0;
            return count;
        }

        #region IDisposable Support

        private bool _disposed; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
                _consumer?.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}