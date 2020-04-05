using System;
using System.Collections.Generic;
using MessageBus.Serializers;
using EasyNetQ;
using EasyNetQ.Topology;

namespace MessageBus.RabbitMQ
{
    public abstract class RabbitMessageBusBase
    {
        private IExchange _deadLetterExchange;
        private IQueue _deadLetterQueue;

        private string _deadLetterRoutingKey;

        protected IAdvancedBus Bus { get; }

        protected QueueConfiguration QueueConfiguration { get; }

        protected ExchangeConfiguration ExchangeConfiguration { get; }

        protected RabbitMessageBusBase(IAdvancedBus bus, ExchangeConfiguration exchangeConfiguration, QueueConfiguration queueConfiguration)
        {
            Bus = bus ?? throw new ArgumentNullException(nameof(bus));
            ExchangeConfiguration = exchangeConfiguration ?? throw new ArgumentNullException(nameof(exchangeConfiguration));
            QueueConfiguration = queueConfiguration ?? throw new ArgumentNullException(nameof(queueConfiguration));

            Setup();
        }

        protected void MoveToDeadLetter(object message, MessageProperties properties, Exception exception)
        {
            var serializedMessage = Serializer.Get(properties.ContentType).Serialize(message);

            properties.Headers.Add("x-death", new Dictionary<string, object>
                {
                    {"count", 1},
                    {"reason", "error"},
                    {"error", exception.Message},
                    {"queue", QueueConfiguration.Name},
                    {"exchange", ExchangeConfiguration.Name },
                    {"routing-keys", QueueConfiguration.RoutingKey},
                    {"time", ((DateTimeOffset) DateTime.UtcNow).ToUnixTimeMilliseconds()}
                }
            );

            Bus.Publish(_deadLetterExchange, _deadLetterRoutingKey, true, properties, serializedMessage);
        }

        private void Setup()
        {
            DefineGenericDeadLetter();
            SetupDeadLetterQueue();
            SetupQueueAndExchange();
        }

        private void DefineGenericDeadLetter()
        {
            var conventions = Bus.Container.Resolve<IConventions>();
            conventions.ErrorExchangeNamingConvention = info => "easynetq.dead.letter.exchange";
            conventions.ErrorQueueNamingConvention = info => "easynetq.dead.letter.queue";
        }

        private void SetupDeadLetterQueue()
        {
            if (QueueConfiguration.DeadLetter is null)
            {
                _deadLetterExchange = Bus.ExchangeDeclare($"dead.letter.{ExchangeConfiguration.Type.ToString()}", ExchangeConfiguration.Type.ToString());
                _deadLetterRoutingKey = $"{QueueConfiguration.RoutingKey}.errors";
                _deadLetterQueue = Bus.QueueDeclare($"{QueueConfiguration.Name}.errors");
            }
            else
            {
                _deadLetterExchange = Bus.ExchangeDeclare(QueueConfiguration.DeadLetter.Name, ExchangeConfiguration.Type.ToString());
                _deadLetterRoutingKey = QueueConfiguration.DeadLetter.RoutingKey;
                _deadLetterQueue = Bus.QueueDeclare(QueueConfiguration.DeadLetter.Name);
            }

            Bus.Bind(_deadLetterExchange, _deadLetterQueue, _deadLetterRoutingKey);
        }

        private void SetupQueueAndExchange()
        {
            var exchange = Bus.ExchangeDeclare(ExchangeConfiguration.Name, ExchangeConfiguration.Type.ToString());
            var queue = Bus.QueueDeclare(QueueConfiguration.Name, exclusive: QueueConfiguration.Exclusive, deadLetterExchange: exchange.Name, deadLetterRoutingKey: _deadLetterRoutingKey);
            Bus.Bind(exchange, queue, QueueConfiguration.RoutingKey);
        }
    }
}