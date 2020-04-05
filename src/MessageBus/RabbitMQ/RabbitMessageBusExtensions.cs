using EasyNetQ;
using MessageBus.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MessageBus.RabbitMQ
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class RabbitMessageBusExtensions
    {
        /// <summary>
        /// Create singleton conection to rabitMq
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, string connectionString) =>
            AddRabbitMQ(services, connectionString, ServiceLifetime.Singleton);

        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, string connectionString, ServiceLifetime serviceLifetime) =>
            AddRabbitMQ(services, connectionString, serviceLifetime, null);

        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, string connectionString, Action<MessageBusOptions> options) =>
            AddRabbitMQ(services, connectionString, ServiceLifetime.Singleton, options);

        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, string connectionString, ServiceLifetime serviceLifetime, Action<MessageBusOptions> options)
        {
            var op = new MessageBusOptions();
            options?.Invoke(op);
            services.AddSingleton(op);
            services.Add(new ServiceDescriptor(typeof(IAdvancedBus), s => RabbitHutch.CreateBus(connectionString).Advanced, serviceLifetime));
            return services;
        }

        public static IServiceCollection AddMessageBusConsumer<T>(this IServiceCollection services, string exchangeName, string queueName, ExchangeTypes exchangeType, string routingKey = "", bool exclusive = false) where T : class =>
            AddMessageBusConsumer<T>(services, new ExchangeConfiguration(exchangeType, exchangeName), new QueueConfiguration(queueName, exclusive, routingKey));

        public static IServiceCollection AddMessageBusConsumer<T>(this IServiceCollection services, ExchangeConfiguration exchangeConfiguration, QueueConfiguration queueConfiguration) where T : class =>
            AddMessageBusConsumer<T>(services, exchangeConfiguration, queueConfiguration, ServiceLifetime.Singleton);

        public static IServiceCollection AddMessageBusConsumer<T>(this IServiceCollection services, ExchangeConfiguration exchangeConfiguration, QueueConfiguration queueConfiguration, ServiceLifetime serviceLifetime) where T : class
        {
            services.Add(new ServiceDescriptor(typeof(IMessageBusConsumer<T>), s => new RabbitMessageBusConsumer<T>(s.GetRequiredService<IAdvancedBus>(), exchangeConfiguration, queueConfiguration, s.GetRequiredService<ILogger<RabbitMessageBusConsumer<T>>>(), s.GetRequiredService<MessageBusOptions>()), serviceLifetime));
            return services;
        }

        public static IServiceCollection AddMessageBusPublisher<T>(this IServiceCollection services, string exchangeName, string queueName, ExchangeTypes exchangeType, string routingKey = "", bool exclusive = false) where T : class =>
            AddMessageBusPublisher<T>(services, new ExchangeConfiguration(exchangeType, exchangeName), new QueueConfiguration(queueName, exclusive, routingKey));

        public static IServiceCollection AddMessageBusPublisher<T>(this IServiceCollection services, ExchangeConfiguration exchangeConfiguration, QueueConfiguration queueConfiguration) where T : class =>
            AddMessageBusPublisher<T>(services, exchangeConfiguration, queueConfiguration, ServiceLifetime.Singleton);

        public static IServiceCollection AddMessageBusPublisher<T>(this IServiceCollection services, ExchangeConfiguration exchangeConfiguration, QueueConfiguration queueConfiguration, ServiceLifetime serviceLifetime) where T : class
        {
            services.Add(new ServiceDescriptor(typeof(IMessageBusPublisher<T>), s => new RabbitMessageBusPublisher<T>(s.GetRequiredService<IAdvancedBus>(), exchangeConfiguration, queueConfiguration, s.GetRequiredService<MessageBusOptions>()), serviceLifetime));
            return services;
        }
    }
}