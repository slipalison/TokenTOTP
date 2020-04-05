using EasyNetQ;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageBus
{
    public interface IMessageBusPublisher<T>
        where T : class
    {
        Task PublishAsync(T message, string contentType = null, string correlationId = null, IDictionary<string, object> headers = null);

        Task PublishAsync(T message, MessageProperties properties);
    }
}