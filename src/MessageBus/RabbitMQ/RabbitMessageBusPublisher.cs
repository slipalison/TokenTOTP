using MessageBus.Options;
using MessageBus.Serializers;
using EasyNetQ;
using EasyNetQ.Topology;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageBus.RabbitMQ
{
    public class RabbitMessageBusPublisher<T> : RabbitMessageBusBase, IMessageBusPublisher<T>
        where T : class
    {
        private readonly MessageBusOptions _options;

        public RabbitMessageBusPublisher(IAdvancedBus bus, ExchangeConfiguration exchangeConfiguration, QueueConfiguration queueConfiguration, MessageBusOptions options)
            : base(bus, exchangeConfiguration, queueConfiguration)
        {
            _options = options;
        }

        public Task PublishAsync(T message, string contentType = null, string correlationId = null, IDictionary<string, object> headers = null)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = _options.DefaultSerializer;
            }
            if (string.IsNullOrWhiteSpace(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            var messageProperties = new MessageProperties
            {
                ContentType = contentType,
                ContentTypePresent = true,
                CorrelationId = correlationId,
                CorrelationIdPresent = true
            };

            if (headers is null) return PublishAsync(message, messageProperties);

            foreach (var item in headers)
                messageProperties.Headers.Add(item);

            return PublishAsync(message, messageProperties);
        }

        public Task PublishAsync(T message, MessageProperties properties)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            if (string.IsNullOrEmpty(properties.ContentType))
                throw new ArgumentException("ContentType should not be null or empty");

            if (properties.ContentType != Serializer.Json.ContentType && properties.ContentType != Serializer.Protobuf.ContentType)
                throw new ArgumentException($"ContentType should have a valid value like {Serializer.Json.ContentType} or {Serializer.Protobuf.ContentType}");

            return PublishInternalAsync(message, properties);
        }

        private async Task PublishInternalAsync(T message, MessageProperties properties)
        {
            var serializer = Serializer.Get(properties.ContentType);
            var serializedMessage = serializer.Serialize(message);

            var exchange = new Exchange(ExchangeConfiguration.Name);

            await Bus.PublishAsync(exchange, QueueConfiguration.RoutingKey, true, properties, serializedMessage);
        }
    }
}